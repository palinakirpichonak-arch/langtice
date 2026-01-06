namespace MainService.DAL.Services;

public interface IMigrationService
{
    Task ApplyMigrationsAsync(CancellationToken cancellationToken);
}