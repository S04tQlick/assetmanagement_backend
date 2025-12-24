using System.Security.Claims;
using AssetManagement.API.DAL.Repositories.UsersRepository;
using AssetManagement.API.DAL.Services.TokenService;
using AssetManagement.Entities.DTOs.Auth.Request;
using AssetManagement.Entities.DTOs.Auth.Response;
using AssetManagement.Entities.DTOs.Auth.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ITokenService tokenService, IUserRepository userRepo, IOptions<JwtSettings> jwtOptions) : ControllerBase
{
    // you should have user repo
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Replace with your user/password verification & password hashing
        var user = await userRepo.GetByEmailAsync(request.Email);
        if (user == null) return Unauthorized("Invalid credentials");
        if (user.IsActive) return Forbid("Account disabled");

        // TODO: verify password hash
        var passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!passwordMatches) return Unauthorized("Invalid credentials");

        // Get user's institution membership (assume user has a single institution in our mapping)
        var instUser = await userRepo.GetUserByIdAndInstitutionIdAsync(user.InstitutionId, user.Id);
        if (instUser == null) return Forbid("No institution associated");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim("InstitutionId", instUser.InstitutionId.ToString()),
            //new Claim("role", instUser.Role)
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await tokenService.StoreRefreshTokenAsync(refreshToken, user.Id, instUser.InstitutionId, refreshExpiry);

        var response = new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        };

        // OPTION: set refresh token as HttpOnly cookie instead of returning in body
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refreshExpiry
        });

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenResponse request)
    {
        // request.RefreshToken present
        if (string.IsNullOrEmpty(request.RefreshToken)) return BadRequest("Refresh token required");

        // Option: extract user id from access token (even if expired) or from refresh token store:
        var principal = tokenService.ValidateAccessToken(request.AccessToken, validateLifetime: false);
        if (principal == null) return Unauthorized();

        var userIdStr = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized();

        var valid = await tokenService.ValidateRefreshTokenAsync(request.RefreshToken, userId);
        if (!valid) return Unauthorized();

        // create new tokens
        var claims = principal.Claims;
        var newAccess = tokenService.GenerateAccessToken(claims);
        var newRefresh = tokenService.GenerateRefreshToken();

        var newExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await tokenService.StoreRefreshTokenAsync(newRefresh, userId,
            Guid.Parse(principal.FindFirst("institution_id")!.Value), newExpiry);

        // revoke old
        await tokenService.RevokeRefreshTokenAsync(request.RefreshToken);

        Response.Cookies.Append("refreshToken", newRefresh, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = newExpiry
        });

        return Ok(new TokenResponse
        {
            AccessToken = newAccess,
            RefreshToken = newRefresh,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] TokenResponse request)
    {
        // revoke refresh token
        if (!string.IsNullOrEmpty(request.RefreshToken))
        {
            await tokenService.RevokeRefreshTokenAsync(request.RefreshToken);
        }

        // clear cookie
        Response.Cookies.Delete("refreshToken");
        return NoContent();
    }
}