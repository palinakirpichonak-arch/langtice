using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MainService.BLL.Resilience;

public static class ResiliencePipelinesConfiguration
{
    public static IServiceCollection ConfigureResilience(this IServiceCollection services)
    {
        services.AddResiliencePipeline("retry", builder =>
        {
            builder.AddDefaultRetry();
        });

        return services;
    }
}