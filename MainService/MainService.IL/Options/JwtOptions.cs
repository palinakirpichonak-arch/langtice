namespace MainService.BLL.Options;

public class JwtOptions
{
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public string AccessSecretKey { get; init; } = "";
}