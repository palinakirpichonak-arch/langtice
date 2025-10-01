namespace MainService.DAL.Abstractions;

public interface IMongoRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken );
    Task<T> GetByIdAsync(string id,CancellationToken cancellationToken );
    Task AddAsync(T entity,CancellationToken cancellationToken );
    Task UpdateAsync(T entity, CancellationToken cancellationToken );
    Task DeleteAsync(string id,CancellationToken cancellationToken );
}