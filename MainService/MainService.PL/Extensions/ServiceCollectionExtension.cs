using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Lessons.Services;
using MainService.AL.Features.Translations.DTO;
using MainService.AL.Features.Translations.Services;
using MainService.AL.Features.Words.DTO;
using MainService.AL.Features.Words.Services;
using MainService.AL.Words.Interfaces;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Data.Translations.Repository;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Translations.Models;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Services;
using MainService.IL.Translations.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MainService.PL.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PostgreLangticeContext>(options =>
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var db = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASS");

            var connectionString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";
            options.UseNpgsql(connectionString);
        });
        
        services.AddDbContext<MongoLangticeContext>(options =>
        {
            var connection = Environment.GetEnvironmentVariable("MONGO_CONNECTION");
            var database = Environment.GetEnvironmentVariable("MONGO_DB");

            var client = new MongoClient(connection);
            options.UseMongoDB(client, database);
        });
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddHostedService<MigrationHostedService>();

        //Repositories (DAL)
        services.AddScoped<IWordRepository, WordRepository>();
        services.AddScoped<IUserWordRepository, UserWordRepository>();
        services.AddScoped<ITranslationRepository, TranslationRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ITestRepository, TestRepository>();

        // Services (IL)
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IUserWordService, UserWordService>();
        services.AddScoped<ITranslationService, TranslationService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ITestService, TestService>();
        
        //Controllers
        services.AddControllers();
    }
}