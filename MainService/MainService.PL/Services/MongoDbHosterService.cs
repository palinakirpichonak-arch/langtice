using MainService.DAL.Context.MongoDb;

namespace MainService.PL.Services;

public class MongoDbHosterService : IHostedService
{
    private readonly MongoDbContext _mongoDbContext;

    public MongoDbHosterService(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mongoDbContext.InitializeDbContext(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}