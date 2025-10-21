using MainService.BLL.Services.LLM;

namespace MainService.PL.Extensions;

public static class HttpClientConfiguration
{
    public static IServiceCollection ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<ILlmClient, Llm>();
        
        return services;
    }
}