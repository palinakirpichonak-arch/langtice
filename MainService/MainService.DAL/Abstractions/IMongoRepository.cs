using MongoDB.Bson;

namespace MainService.DAL.Abstractions;

public interface IMongoRepository<T, TKey> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken );
    Task<T> GetByIdAsync(TKey id,CancellationToken cancellationToken );
    Task AddAsync(T entity,CancellationToken cancellationToken );
    Task UpdateAsync(T entity, CancellationToken cancellationToken );
    Task DeleteAsync(TKey id,CancellationToken cancellationToken );
}