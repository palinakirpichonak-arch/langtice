using AuthService.AL.Extensions;
using AuthService.DAL.Extensions;
using AuthService.IL.Extensions;
using AuthService.IL.gRPC;
using AuthService.PL.Auth;
using AuthService.PL.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddConnectExternalsExtension(builder.Configuration);
builder.Services.AddDataAccess();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
    
    options.ListenAnyIP(7071, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAuthEndpoints();
app.MapGrpcService<GrpcServer>();
app.Run();

