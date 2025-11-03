using AuthService.AL.Features.Users.Dto;

namespace AuthService.AL.Features;

public interface IAppAuthService
{
    Task<(string accessToken, string refreshToken)> LoginAsync(LoginUserDto dto, CancellationToken ct);
    Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken, CancellationToken ct);
    Task LogoutAsync(string refreshToken, CancellationToken ct);
}