namespace MainService.BLL.Options;

public class RabbitMqOptions
{
    public string HostName { get; init; } 
    public int Port { get; init; }
    public string UserName { get; init; } 
    public string Password { get; init; } 
    public string Queue { get; init; } 
    public string Exchange { get; init; }     
    public string RoutingKey { get; init; }
}