using AuthService.IL.Options;
using AuthService.IL.Services;
using AuthService.IL.Sessions;
using AuthService.IL.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.IL.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<RedisOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ISessionStore, SessionStore>();
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        
        return services;
    }
}