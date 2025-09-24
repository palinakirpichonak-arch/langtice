using MainService.PL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext();
builder.Services.ConfigureServices();

var app = builder.Build();
app.MapControllers();
app.Run();

