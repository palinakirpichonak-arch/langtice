using MainService.DAL;
using MainService.DAL.Models;
using MainService.DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace MainService.PL.Extensions;

public static class ServiceCollectionExtension
{
    public static void SetDbContext(this IServiceCollection services)
    {
        services.AddDbContext<LangticeContext>(options =>
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var db = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASS");

            var connectionString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";
            options.UseNpgsql(connectionString);
        });
    }
    public static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
    }
}