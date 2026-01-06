using MainService.AL.Extensions;
using MainService.BLL.Extension;
using MainService.BLL.Resilience;
using MainService.DAL.Extensions;
using MainService.PL.Extensions;
using MainService.PL.Middlewares;
using MainService.PL.Services;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
var services = builder.Services;

services
    .ConfigureOptions(builder.Configuration)
    .ConfigureHttpClient()
    .ConfigureResilience()
    .ConfigureDbContext()
    .ConfigureMigrations()
    .ConfigureUnitOfWork()
    .AddInfrastructureServices()
    .ConfigureHostedServices()
    .ConfigureRepositories()
    .ConfigureApplicationServices()
    .AddApiAuthentication(builder.Configuration)
    .ConfigureControllers()
    .ConfigureMappers()
    .ConfigureSwagger();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddConsole();
    var services = builder.Services;
    
    services
        .ConfigureOptions(builder.Configuration)
        .ConfigureHttpClient()
        .ConfigureResilience()
        .ConfigureDbContext()
        .ConfigureMigrations()
        .ConfigureUnitOfWork()
        .ConfigureHostedServices()
        .ConfigureRepositories()
        .ConfigureApplicationServices()
        .AddApiAuthentication(builder.Configuration)
        .ConfigureControllers()
        .ConfigureMappers()
        .ConfigureSwagger();

    var app = builder.Build();
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks("/health");
    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    logger.Fatal(e.ToString(), "Fatal error in MainService");
    throw;
}
finally
{
    LogManager.Shutdown();
}


