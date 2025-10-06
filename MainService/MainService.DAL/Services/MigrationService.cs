using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Services;

public class MigrationService : IMigrationService
{
    private readonly PostgreDbContext _dbContext;

    public MigrationService(PostgreDbContext dbcontext)
    {
        _dbContext = dbcontext;
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
                Console.WriteLine("No pending migrations found.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception while applying migrations: {e.Message}");
            throw;
        }
    }
    
}