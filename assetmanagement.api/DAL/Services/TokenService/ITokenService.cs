using System.Security.Claims;

namespace AssetManagement.API.DAL.Services.TokenService;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateAccessToken(string token, bool validateLifetime = true);
    Task StoreRefreshTokenAsync(string refreshToken, Guid userId, Guid institutionId, DateTime expiry);
    Task<bool> ValidateRefreshTokenAsync(string refreshToken, Guid userId);
    Task RevokeRefreshTokenAsync(string refreshToken);
}