using MainService.DAL;
using MainService.DAL.Models;
using MainService.DAL.Services;
using MainService.PL.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetDbContext();
builder.Services.SetServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
    await migrationService.ApplyMigrationsAsync();
}

app.MapGet("/db", (LangticeContext dbContext) =>
{
    var db = dbContext.Database.GetDbConnection().Database;
    return Results.Ok(db);
});
app.Run();

