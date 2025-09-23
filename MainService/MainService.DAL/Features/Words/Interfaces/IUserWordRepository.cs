public interface IUserWordRepository<T> where T : class
{
    Task<T?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}