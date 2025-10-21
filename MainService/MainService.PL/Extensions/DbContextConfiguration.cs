using MainService.BLL.Services.Options;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Context.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainService.PL.Extensions;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PostgreDbContext>((sp, options) =>
        {
            var pgOptions = sp.GetRequiredService<IOptions<PostgreOptions>>().Value;
            options.UseNpgsql(pgOptions.ConnectionString);
        });

        services.AddSingleton<MongoDbContext>(sp =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return  new MongoDbContext(mongoOptions.MongoConnection, mongoOptions.MongoDatabase);
        });
        
        return services;
    }
}