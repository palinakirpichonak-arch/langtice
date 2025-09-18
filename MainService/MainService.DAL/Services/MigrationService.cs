using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Services;

public class MigrationService : IMigrationService
{
    private readonly LangticeContext _dbContext;

    public MigrationService(LangticeContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task ApplyMigrationsAsync()
    {
        try
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
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