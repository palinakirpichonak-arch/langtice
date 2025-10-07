namespace MainService.DAL.Abstractions;

public interface IMigrationService
{
    Task ApplyMigrationsAsync(CancellationToken cancellationToken);
}