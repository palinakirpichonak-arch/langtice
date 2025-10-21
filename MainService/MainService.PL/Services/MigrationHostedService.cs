using MainService.DAL.Abstractions;
using Polly.Registry;

namespace MainService.PL.Services;

public class MigrationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MigrationHostedService> _logger;
    private readonly ResiliencePipelineProvider<string> _pipelineProvider;

    public MigrationHostedService(
        IServiceProvider serviceProvider,
        ILogger<MigrationHostedService> logger,
        ResiliencePipelineProvider<string> pipelineProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _pipelineProvider = pipelineProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
        var pipeline = _pipelineProvider.GetPipeline("retry");
        try
        {
            await pipeline.ExecuteAsync(async ct => await migrationService.ApplyMigrationsAsync(ct), cancellationToken);
        }
        catch (Exception e)
        {
           _logger.LogError($"Error while applying migrations: {e.Message}");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}