using MainService.AL.Features.Courses.Services;
using MainService.AL.Features.Languages.Services;
using MainService.AL.Features.Lessons.Services;
using MainService.AL.Features.Translations.Services;
using MainService.AL.Features.Words.Services;
using MainService.AL.Mappers;
using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Data.Translations.Repository;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Services;
using MainService.PL.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MainService.PL.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PostgreDbContext>(options =>
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var db = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASS");

            var connectionString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";
            options.UseNpgsql(connectionString);
        });
        
        services.AddSingleton<MongoDbContext>(sp =>
            new MongoDbContext(
                Environment.GetEnvironmentVariable("MONGO_CONNECTION"),
                Environment.GetEnvironmentVariable("MONGO_DB")
            ));
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddHostedService<MigrationHostedService>();

        //Repositories (DAL)
        services.AddScoped<IWordRepository, WordRepository>();
        services.AddScoped<IUserWordRepository, UserWordRepository>();
        services.AddScoped<ITranslationRepository, TranslationRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IMongoRepository<Test, string>>(sp =>
            new MongoRepository<Test, string>(sp.GetRequiredService<MongoDbContext>(), "tests"));

        // Services (AL)
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IUserWordService, UserWordService>();
        services.AddScoped<ITranslationService, TranslationService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUserCourseService, UserCourseService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ITestService, TestService>();
        
        //Controllers
        services.AddControllers();

        //Mappers
        services.AddMapster();
        MapsterConfig.Configure();
        
        //Swagger
       services.AddEndpointsApiExplorer();
       services.AddSwaggerGen();
    }
}