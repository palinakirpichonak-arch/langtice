using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using AuthService.DAL.Features.Users.Models;
using AuthService.DAL.Features.Users.Repositories;
using AuthService.IL.Options;
using AuthService.IL.Sessions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.IL.Tokens;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly JwtOptions _opt;
    private readonly ISessionStore _sessions;
    private readonly IUserRepository _users;
    private readonly IAccessTokenService _access;
    private readonly SigningCredentials _creds;
    private readonly TokenValidationParameters _validationParams;

    public RefreshTokenService(
        IOptions<JwtOptions> opt,
        ISessionStore sessions,
        IUserRepository users,
        IAccessTokenService access)
    {
        _opt = opt.Value;
        _sessions = sessions;
        _users = users;
        _access = access;

        var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.RefreshSecretKey));
        _creds = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);

        _validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _opt.Issuer,

            ValidateAudience = true,
            ValidAudience = _opt.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = refreshKey
        };
    }

    private (string token, string jti) CreateRefreshToken(Guid userId)
    {
        var jti = Guid.NewGuid().ToString("N");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, jti)
        };

        var jwt = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_opt.RefreshExpiresDays),
            signingCredentials: _creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
        return (tokenString, jti);
    }

    private (Guid userId, string jti) ValidateRefreshTokenSignature(string refreshToken)
    {
        var handler = new JwtSecurityTokenHandler(); 

        ClaimsPrincipal principal;
        try
        {
            principal = handler.ValidateToken(refreshToken, _validationParams, out _);
        }
        catch
        {
            throw new SecurityException("Invalid or expired refresh token");
        }
        
        var sub = principal.FindFirst("sub")?.Value
                  ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var jti = principal.FindFirst("jti")?.Value
                  ?? principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        if (string.IsNullOrEmpty(sub) || string.IsNullOrEmpty(jti))
            throw new SecurityException("Malformed refresh token");

        if (!Guid.TryParse(sub, out var userId))
            throw new SecurityException("Invalid userId in refresh");

        return (userId, jti);
    }

    public async Task<(string refreshToken, Guid familyId)> IssueInitialAsync(User user, CancellationToken ct)
    {
        var familyId = Guid.NewGuid();

        var (refreshToken, jti) = CreateRefreshToken(user.Id);

        var record = new RefreshTokenState()
        {
            UserId = user.Id,
            FamilyId = familyId,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_opt.RefreshExpiresDays),
            Rotated = false
        };

        await _sessions.StoreAsync(
            jti,
            record,
            TimeSpan.FromDays(_opt.RefreshExpiresDays),
            ct);

        return (refreshToken, familyId);
    }

    public async Task<(User user, string newAccessToken, string newRefreshToken)> RotateAsync(string oldRefreshToken, CancellationToken ct)
    {
        var (userId, oldJti) = ValidateRefreshTokenSignature(oldRefreshToken);
        
        var session = await _sessions.GetAsync(oldJti, ct);
        if (session is null)
            throw new SecurityException("Session not found or revoked");
        
        if (session.Rotated)
        {
            await _sessions.RevokeFamilyAsync(session.FamilyId, ct);
            throw new SecurityException("Refresh reuse detected. Session revoked.");
        }

        if (DateTime.UtcNow > session.ExpiresAtUtc)
        {
            await _sessions.RevokeFamilyAsync(session.FamilyId, ct);
            throw new SecurityException("Refresh expired");
        }

     
        await _sessions.MarkRotatedAsync(oldJti, ct);
        
        var (newRefreshToken, newJti) = CreateRefreshToken(userId);

        var newRecord = new RefreshTokenState()
        {
            UserId = userId,
            FamilyId = session.FamilyId,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_opt.RefreshExpiresDays),
            Rotated = false
        };

        await _sessions.StoreAsync(
            newJti,
            newRecord,
            TimeSpan.FromDays(_opt.RefreshExpiresDays),
            ct);
        
        var user = await _users.GetByIdAsync(userId, ct)
                   ?? throw new SecurityException("User not found");

        var newAccessToken = await _access.GenerateAccessToken(user, ct);

        return (user, newAccessToken, newRefreshToken);
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken ct)
    {
        var (userId, jti) = ValidateRefreshTokenSignature(refreshToken);

        var session = await _sessions.GetAsync(jti, ct);
        if (session is null)
            return;
        
        await _sessions.RevokeFamilyAsync(session.FamilyId, ct);
    }
}