using MainService.PL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppOptions(builder.Configuration);
builder.Services.ConfigureDbContext();
builder.Services.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

