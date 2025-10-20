using MainService.AL.Features.Courses.Services;
using MainService.AL.Features.Languages.Services;
using MainService.AL.Features.Lessons.Services;
using MainService.AL.Features.Tests.Services;
using MainService.AL.Features.Translations.Services;
using MainService.AL.Features.UserCourse.Service;
using MainService.AL.Features.UserFlashCards.Services;
using MainService.AL.Features.UserTests.Services;
using MainService.AL.Features.UserWords.Services;
using MainService.AL.Features.Words.Services;
using MainService.AL.Mappers;
using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Data.Tests;
using MainService.BLL.Data.Translations;
using MainService.BLL.Data.UserCourses;
using MainService.BLL.Data.UserWord;
using MainService.BLL.Data.Words;
using MainService.BLL.Services.Options;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.Test;
using MainService.DAL.Services;
using MainService.PL.Services;
using MainService.PL.Services.Options;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainService.PL.Extensions;

public static class ServiceCollectionExtension
{
    
    public static void ConfigureAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // PostgreSQL
        services.Configure<PostgreOptions>(configuration);

        // MongoDB
        services.Configure<MongoOptions>(configuration);
        
        //LLM
        services.Configure<LlmOptions>(configuration);
    }
    
    public static void ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PostgreDbContext>((sp, options) =>
        {
            var pgOptions = sp.GetRequiredService<IOptions<PostgreOptions>>().Value;
            options.UseNpgsql(pgOptions.ConnectionString);
        });

        services.AddSingleton<MongoDbContext>(sp =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoDbContext(mongoOptions.MongoConnection, mongoOptions.MongoDatabase);
        });
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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
        services.AddScoped<IUserFlashCardsService, UserFlashCardsService>();
        services.AddScoped<IUserTestService, UserTestService>();
        
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