using AuthService.AL.Extensions;
using AuthService.DAL.Extensions;
using AuthService.IL.Extensions;
using AuthService.PL.Auth;
using AuthService.PL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConnectExternalsExtension(builder.Configuration);
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

app.MapAuthEndpoints();

app.Run();

