using AuthService.PL.Services;
using MainService.BLL.Services.gRPC;
using MainService.BLL.Services.RabbitMq;
using MainService.PL.Services;
using MainService.PL.Services.gRPC;

namespace MainService.PL.Extensions;

public static class HostedServicesConfiguration
{
    public static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<MigrationHostedService>();
        services.AddHostedService<MongoDbHostedService>();
        services.AddHostedService<RabbitSenderService>();

        services.AddHttpContextAccessor();
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<IGrpcClient, GrpcClient>();
       
        return services;
    }
}