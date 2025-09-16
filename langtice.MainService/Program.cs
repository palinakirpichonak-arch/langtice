using langtice_api.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var username = configuration["postgres-username"];
var password = configuration["postgres-password"];
var connectionString = configuration
    .GetConnectionString("Database")
    .Replace("{USERNAME}", username)
    .Replace("{PASSWORD}", password);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<LangticeDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();