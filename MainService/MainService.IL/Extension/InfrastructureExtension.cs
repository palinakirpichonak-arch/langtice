using MainService.BLL.Services.gRPC;
using MainService.BLL.Services.RabbitMq;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.BLL.Extension;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IGrpcClient, GrpcClient>();
        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
        
        return services;
    }
}