using AuthService.DAL.Users;

namespace AuthService.IL.Services;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}