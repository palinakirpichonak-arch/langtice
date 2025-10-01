using MainService.AL.Mappers;

public interface IMongoService<TEntity, TDto>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(string id, TDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}