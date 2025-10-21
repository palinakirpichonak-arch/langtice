using MainService.BLL.Services.LLM;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.BLL;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        return services.AddScoped<ILlmClient, Llm>();
    }
}