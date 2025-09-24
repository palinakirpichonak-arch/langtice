using MainService.DAL.Models;

namespace MainService.DAL;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllItemsByAsync(CancellationToken cancellationToken);
    Task AddItemAsync(TEntity item, CancellationToken cancellationToken);
    Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken);
    Task DeleteItemAsync(TEntity item, CancellationToken cancellationToken); 
}