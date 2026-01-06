using AuthService.DAL.Features.Users.Models;

namespace AuthService.IL.Tokens;

public interface IAccessTokenService
{
    Task<string> GenerateAccessToken(User user, CancellationToken cancellationToken);
}