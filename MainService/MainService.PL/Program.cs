using MainService.AL.Extensions;
using MainService.BLL;
using MainService.BLL.Resilience;
using MainService.DAL.Extensions;
using MainService.PL.Extensions;
using MainService.PL.Middlewares;

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

app.MapControllers();
app.Run();

