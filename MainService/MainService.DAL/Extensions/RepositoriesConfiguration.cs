using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Models.TestModel;
using MainService.DAL.Models.UserFlashCardModel;
using MainService.DAL.Repositories.Courses;
using MainService.DAL.Repositories.Languages;
using MainService.DAL.Repositories.Lessons;
using MainService.DAL.Repositories.Tests;
using MainService.DAL.Repositories.Translations;
using MainService.DAL.Repositories.UserCourses;
using MainService.DAL.Repositories.UserFlashCards;
using MainService.DAL.Repositories.UserStreaks;
using MainService.DAL.Repositories.UserTests;
using MainService.DAL.Repositories.UserWords;
using MainService.DAL.Repositories.Words;
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
        services.AddScoped<IUserTestRepository, UserTestRepository>();
        services.AddScoped<IUserFlashCardsRepository, UserFlashCardsRepository>();
        services.AddScoped<IUserStreakRepository, UserStreakRepository>();
        
        services.AddScoped<IMongoRepository<Test, string>>(sp =>
            new MongoRepository<Test, string>(sp.GetRequiredService<MongoDbContext>(), MongoDbCollections.TestsCollectionName)); 
        services.AddScoped<IMongoRepository<UserFlashCard, string>>(sp =>
            new MongoRepository<UserFlashCard, string>(sp.GetRequiredService<MongoDbContext>(), MongoDbCollections.FlashCardsCollectionName));
        
        return services;
    }
}