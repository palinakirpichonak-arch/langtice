using MainService.DAL.Context.MongoDb;

namespace MainService.PL.Services;

public class MongoDbHostedService : IHostedService
{
    private readonly MongoDbContext _mongoDbContext;

    public MongoDbHostedService(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mongoDbContext.InitializeDbContext(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}