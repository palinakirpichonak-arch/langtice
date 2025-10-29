using AuthService.AL.Extensions;
using AuthService.DAL.Abstractions;
using AuthService.DAL.Extensions;
using AuthService.IL.Extensions;
using AuthService.PL.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDapperDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connString = config.GetConnectionString("DefaultConnection");
    
    return new PostgreDbConnection(connString);
});

builder.Services.AddDataAccess();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUsersEndpoints();

app.Run();

