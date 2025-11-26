namespace MainService.PL.Services.gRPC;

public interface IGrpcClient
{
    Task<string> SendMessage(CancellationToken cancellationToken);
}