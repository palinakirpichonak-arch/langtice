using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Data.Courses;
using MainService.DAL.Data.Languages;
using MainService.DAL.Data.Lessons;
using MainService.DAL.Data.Tests;
using MainService.DAL.Data.Translations;
using MainService.DAL.Data.UserCourses;
using MainService.DAL.Data.UserWord;
using MainService.DAL.Data.Words;
using MainService.DAL.Features.Test;
using MainService.DAL.Features.UserFlashCard;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.DAL.Extensions;

public static class RepositoriesConfiguration
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWordRepository, WordRepository>();
        services.AddScoped<IUserWordRepository, UserWordRepository>();
        services.AddScoped<ITranslationRepository, TranslationRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        
        services.AddScoped<IMongoRepository<Test, string>>(sp =>
            new MongoRepository<Test, string>(sp.GetRequiredService<MongoDbContext>(), MongoDbCollections.TestsCollectionName)); 
        services.AddScoped<IMongoRepository<UserFlashCards, string>>(sp =>
            new MongoRepository<UserFlashCards, string>(sp.GetRequiredService<MongoDbContext>(), MongoDbCollections.FlashCardsCollectionName));
        
        return services;
    }
}