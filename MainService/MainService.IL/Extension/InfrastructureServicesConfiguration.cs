using MainService.BLL.Services.LLM;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.BLL;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ILlmClient, Llm>();
        return services;
    }
}