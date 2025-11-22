using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton(_ =>
{
    var uri = new Uri("amqp://guest:guest@rabbit:5672/CUSTOM_HOST");
    return new ConnectionFactory
    {
        Uri = uri,
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

