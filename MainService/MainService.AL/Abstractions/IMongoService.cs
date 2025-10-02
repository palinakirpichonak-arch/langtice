namespace MainService.AL.Abstractions;

public interface IMongoService<TEntity, TDto, TKey>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TKey id, TDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken);
}