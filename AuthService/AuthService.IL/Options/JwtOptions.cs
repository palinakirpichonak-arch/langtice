namespace AuthService.IL.Options;

public sealed class JwtOptions
{
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";

    public string AccessSecretKey { get; init; } = "";
    public int AccessExpiresHours { get; init; } = 1;

    public string RefreshSecretKey { get; init; } = "";
    public int RefreshExpiresDays { get; init; } = 14;
}