using AuthService.DAL.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DAL.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}