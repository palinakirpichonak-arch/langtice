using AuthService.AL.Extensions;
using AuthService.DAL.Extensions;
using AuthService.IL.Extensions;
using AuthService.PL.Auth;
using AuthService.PL.Extensions;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{

    Log.Information("Init AuthService");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(new RenderedCompactJsonFormatter()));
    
    builder.Services.AddConnectExternalsExtension(builder.Configuration);
    builder.Services.AddDataAccess();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapAuthEndpoints();

    app.Run();

}

catch (Exception ex)
{
    Log.Fatal(ex,"Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


