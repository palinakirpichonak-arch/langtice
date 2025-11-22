using MainService.BLL.Options;

namespace MainService.PL.Extensions;

public static class OptionsConfiguration
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        //JwtOptions
        services.AddOptions<JwtOptions>()
            .Bind(configuration)
            .ValidateOnStart();
        
        // PostgreSQL
        services.AddOptions<PostgreOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // MongoDB
        services.AddOptions<MongoOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        //LLM
        services.AddOptions<LlmOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}