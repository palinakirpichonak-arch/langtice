using NotificationService.IL.Options;
using Shared.Options;

namespace NotificationService.PL.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<EmailOptions>()
            .Bind(configuration.GetSection("EmailSettings"));
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection("RabbitMq")) 
            .ValidateOnStart();
        
        return services;
    }
}