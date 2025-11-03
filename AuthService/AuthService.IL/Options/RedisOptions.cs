namespace AuthService.IL.Options;

public class RedisOptions
{
    public string RedisHost { get; init; } = string.Empty;
    public int RedisPort { get; init; }
    public string RedisPassword { get; init; } = string.Empty;
    public string RedisConnectionString { get; init; } = string.Empty;
}