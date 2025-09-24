public interface IKeysRepository<T> where T : class
{
    Task<T?> GetByIdsAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllByUserIdAsync(Guid firstId, CancellationToken cancellationToken);
}