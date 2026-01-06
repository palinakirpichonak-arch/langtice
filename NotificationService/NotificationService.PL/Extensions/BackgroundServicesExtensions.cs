using NotificationService.IL.Services.RabbitMq;

namespace NotificationService.PL.Extensions;

public static class BackgroundServicesExtensions
{
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<RabbitConsumerService>();
        
        return services;
    }
}