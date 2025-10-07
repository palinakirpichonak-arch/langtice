namespace MainService.DAL.Abstractions;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllItemsAsync(CancellationToken cancellationToken);
    Task<PaginatedList<TEntity>> GetAllItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PaginatedList<TEntity>> GetAllItemsWithIdAsync(Guid id,int pageIndex, int pageSize, CancellationToken cancellationToken);
    
    Task AddItemAsync(TEntity item, CancellationToken cancellationToken);
    Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken);
    Task DeleteItemAsync(TEntity item, CancellationToken cancellationToken); 
}