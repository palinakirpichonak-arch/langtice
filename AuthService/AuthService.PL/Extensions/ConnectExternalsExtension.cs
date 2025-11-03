using AuthService.DAL.Abstractions;
using StackExchange.Redis;

namespace AuthService.PL.Extensions;

public static class ConnectExternalsExtension
{
    public static IServiceCollection AddConnectExternalsExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDapperDbConnection>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connString = config.GetConnectionString("DefaultConnection");
    
            return new PostgreDbConnection(connString);
        });

        services.AddSingleton<IConnectionMultiplexer>(options =>
        {
            var config = configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(config);
        });
        
        return services;
    }
}