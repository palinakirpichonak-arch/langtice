using MainService.DAL;
using MainService.PL.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext();
builder.Services.ConfigureServices();

var app = builder.Build();

app.MapGet("/db", (LangticeContext dbContext) =>
{
    var db = dbContext.Database.GetDbConnection().Database;
    return Results.Ok(db);
});
app.Run();

