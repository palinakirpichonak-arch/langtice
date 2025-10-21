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
using Microsoft.Extensions.DependencyInjection;

namespace MainService.AL.Extensions;

public static class ApplicationServicesConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
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
        
        return services;
    }
}