using MainService.AL.Mappers;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.AL.Extensions;

public static class MappersConfiguration
{
    public static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        services.AddMapster();
        MapsterConfig.Configure();
        
        return services;
    }
}