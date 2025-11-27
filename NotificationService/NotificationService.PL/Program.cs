using NotificationService.IL.Options;
using NotificationService.IL.Services;
using NotificationService.IL.Services.RabbitMq;
using NotificationService.IL.Services.Smtp;
using NotificationService.PL;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddOpenApi();


builder.Services.AddOptions<EmailOptions>()
    .Bind(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddOptions<RabbitMqOptions>()
    .Bind(builder.Configuration.GetSection("RabbitMq")) 
    .ValidateOnStart();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddHostedService<RabbitConsumerService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

