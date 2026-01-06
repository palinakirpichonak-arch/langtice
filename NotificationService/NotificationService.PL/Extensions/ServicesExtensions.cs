using NotificationService.IL.Services.Smtp;

namespace NotificationService.PL.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        
        return services;
    }
}