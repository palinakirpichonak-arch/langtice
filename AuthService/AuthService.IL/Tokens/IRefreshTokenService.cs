using AuthService.DAL.Features.Users.Models;

namespace AuthService.IL.Tokens;

public interface IRefreshTokenService
{
    Task<(string refreshToken, Guid familyId)> IssueInitialAsync(User user, CancellationToken ct);
    Task<(User user, string newAccessToken, string newRefreshToken)> RotateAsync(string oldRefreshToken, CancellationToken ct);
    Task LogoutAsync(string refreshToken, CancellationToken ct);
}