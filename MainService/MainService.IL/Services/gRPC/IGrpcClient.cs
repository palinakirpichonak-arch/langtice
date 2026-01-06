namespace MainService.PL.Services.gRPC;

public interface IGrpcClient
{
    Task<string> SendMessage(string userId, CancellationToken cancellationToken);
}