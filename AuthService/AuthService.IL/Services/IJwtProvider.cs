using AuthService.DAL.Users;

namespace AuthService.IL.Services;

public interface IJwtProvider
{
    Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken);
}