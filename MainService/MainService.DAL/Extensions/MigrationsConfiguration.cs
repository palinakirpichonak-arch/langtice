using MainService.DAL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.DAL.Extensions;

public static class MigrationsConfiguration
{
    public static IServiceCollection ConfigureMigrations(this IServiceCollection services)
    {
        return services.AddScoped<IMigrationService, MigrationService>();
    }
}