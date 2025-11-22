namespace NotificationService.PL;

public class RabbitMqOptions
{
    public string HostName { get; init; } = "rabbitmq";
    public int Port { get; init; }
    public string UserName { get; init; } = "guest";
    public string Password { get; init; } = "guest";
    public string Queue { get; init; } = "notification";
    public string Exchange { get; init; } = "";
    public string RoutingKey { get; init; }
}