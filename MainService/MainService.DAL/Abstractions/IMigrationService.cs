namespace MainService.DAL;

public interface IMigrationService
{
    Task ApplyMigrationsAsync(CancellationToken cancellationToken);
}