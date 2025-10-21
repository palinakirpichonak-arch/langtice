using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MainService.DAL.Services;

public class MigrationService : IMigrationService
{
    private readonly PostgreDbContext _dbContext;
    private readonly ILogger<MigrationService> _logger;
    
    public MigrationService(
        PostgreDbContext dbcontext,
        ILogger<MigrationService> logger
        )
    {
        _dbContext = dbcontext;
        _logger = logger;
    }

    public async Task ApplyMigrationsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync(cancellationToken);
            }
            else
            {
                _logger.LogInformation("No pending migrations found.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Exception while applying migrations: {e.Message}");
            throw;
        }
    }
}