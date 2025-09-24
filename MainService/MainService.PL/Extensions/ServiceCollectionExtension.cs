using MainService.AL.Translations.Interfaces;
using MainService.AL.Words.Interfaces;
using MainService.BLL.Translations.Manager;
using MainService.BLL.Translations.Service;
using MainService.BLL.Words.Interfaces;
using MainService.BLL.Words.Manager;
using MainService.BLL.Words.Managers;
using MainService.BLL.Words.Service;
using MainService.DAL;
using MainService.DAL.Models;
using MainService.DAL.Services;
using MainService.DAL.Words.Repository;
using Microsoft.EntityFrameworkCore;

namespace MainService.PL.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureDbContext(this IServiceCollection services)
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
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddHostedService<MigrationHostedService>();

        //Repositories (DAL)
        
        // Managers (BLL)

        // Services (Application Layer)

        //Controllers
        services.AddControllers();
    }
}