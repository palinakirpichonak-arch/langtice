using AuthService.AL.Features;
using AuthService.AL.Features.Users.Services;
using AuthService.IL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.AL.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAppAuthService, AppAuthService>();
        
        return services;
    }
}