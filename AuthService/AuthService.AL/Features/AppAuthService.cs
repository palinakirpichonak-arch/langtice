using System.Security;
using AuthService.AL.Features.Users.Dto;
using AuthService.AL.Features.Users.Services;
using AuthService.IL.Tokens;

namespace AuthService.AL.Features;

public class AppAuthService : IAppAuthService
{
    private readonly IUserService _users;
    private readonly IAccessTokenService _access;
    private readonly IRefreshTokenService _refresh;

    public AppAuthService(
        IUserService users,
        IAccessTokenService access,
        IRefreshTokenService refresh)
    {
        _users = users;
        _access = access;
        _refresh = refresh;
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(LoginUserDto dto, CancellationToken ct)
    {
        var user = await _users.ValidateCredentialsAsync(dto, ct);
        if (user is null)
            throw new SecurityException("Invalid credentials");

        var accessToken = await _access.GenerateAccessToken(user, ct);

        var (refreshToken, _) = await _refresh.IssueInitialAsync(user, ct);

        return (accessToken, refreshToken);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshAsync(string existingRefreshToken, CancellationToken ct)
    {
        var (_, newAccess, newRefresh) = await _refresh.RotateAsync(existingRefreshToken, ct);
        return (newAccess, newRefresh);
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken ct)
    {
        await _refresh.LogoutAsync(refreshToken, ct);
    }
}