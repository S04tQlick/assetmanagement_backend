using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AssetManagement.Entities.DTOs.Auth.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace AssetManagement.API.DAL.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly JwtSettings _settings;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private readonly string _refreshPrefix = "refresh:";

    public TokenService(IOptions<JwtSettings> options, IConnectionMultiplexer redis)
    {
        _settings = options.Value;
        _redis = redis;
        _db = _redis.GetDatabase();
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    private static string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }

    public async Task StoreRefreshTokenAsync(string refreshToken, Guid userId, Guid institutionId, DateTime expiry)
    {
        var hashed = HashToken(refreshToken);
        var key = _refreshPrefix + hashed;

        var data = new
        {
            userId = userId.ToString(),
            institutionId = institutionId.ToString(),
            createdAt = DateTime.UtcNow.ToString("o")
        };

        var json = System.Text.Json.JsonSerializer.Serialize(data);
        var ttl = expiry - DateTime.UtcNow;

        await _db.StringSetAsync(key, json, ttl);
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken, Guid userId)
    {
        var hashed = HashToken(refreshToken);
        var key = _refreshPrefix + hashed;

        var value = await _db.StringGetAsync(key);
        if (!value.HasValue || !value.ToString().StartsWith('{')) return false;

        try
        {
            var doc = System.Text.Json.JsonDocument.Parse(((string)value)!);
            var storedUserId = doc.RootElement.GetProperty("userId").GetString();
            return storedUserId != null && Guid.Parse(storedUserId) == userId;
        }
        catch
        {
            return false;
        }
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var hashed = HashToken(refreshToken);
        var key = _refreshPrefix + hashed;
        await _db.KeyDeleteAsync(key);
    }

    public ClaimsPrincipal? ValidateAccessToken(string token, bool validateLifetime = true)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = !string.IsNullOrEmpty(_settings.Issuer),
                ValidIssuer = _settings.Issuer,
                ValidateAudience = !string.IsNullOrEmpty(_settings.Audience),
                ValidAudience = _settings.Audience,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.FromSeconds(60)
            };

            var principal = handler.ValidateToken(token, parameters, out var validatedToken);
            return principal;
        }
        catch
        {
            return null;
        }
    }
}