using MainService.BLL.Services.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace MainService.BLL.Extension;

public static class UnitOfWorkConfiguration
{
    public static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}