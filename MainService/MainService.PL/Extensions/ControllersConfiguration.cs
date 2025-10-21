namespace MainService.PL.Extensions;

public static class ControllersConfiguration
{
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}