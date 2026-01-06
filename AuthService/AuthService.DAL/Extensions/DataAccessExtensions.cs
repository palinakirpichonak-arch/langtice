using AuthService.DAL.Features.Roles.Repositories;
using AuthService.DAL.Features.Users.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DAL.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        return services;
    }
}