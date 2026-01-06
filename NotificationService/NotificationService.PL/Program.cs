using NotificationService.PL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services
    .AddOptions(builder.Configuration)
    .AddInfrastructureServices()
    .AddBackgroundServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

