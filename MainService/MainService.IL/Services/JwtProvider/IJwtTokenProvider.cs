namespace AuthService.PL.Services;

public interface IJwtTokenProvider
{
    string GetJwtToken();
}