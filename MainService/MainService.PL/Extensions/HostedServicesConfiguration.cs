using MainService.BLL.Services.gRPC;
using MainService.BLL.Services.RabbitMq;
using MainService.PL.Services;

namespace MainService.PL.Extensions;

public static class HostedServicesConfiguration
{
    public static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<MigrationHostedService>();
        services.AddHostedService<MongoDbHostedService>();
        services.AddHostedService<RabbitSenderService>();
        services.AddHostedService<GreeterClient>();
       
        return services;
    }
}