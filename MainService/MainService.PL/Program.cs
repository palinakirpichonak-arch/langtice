using MainService.AL.Extensions;
using MainService.BLL;
using MainService.DAL.Extensions;
using MainService.PL.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .ConfigureOptions(builder.Configuration)
    .ConfigureDbContext()
    .ConfigureMigrations()
    .ConfigureUnitOfWork()
    .ConfigureHostedServices()
    .ConfigureRepositories()
    .ConfigureInfrastructureServices()
    .ConfigureApplicationServices()
    .ConfigureControllers()
    .ConfigureMappers()
    .ConfigureSwagger();

builder.Services.ConfigureApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

