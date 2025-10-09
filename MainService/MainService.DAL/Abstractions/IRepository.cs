namespace MainService.DAL.Abstractions;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllItemsAsync(CancellationToken cancellationToken);
    Task<PaginatedList<TEntity>> GetAllItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PaginatedList<TEntity>> GetAllItemsWithIdAsync(TKey id,int pageIndex, int pageSize, CancellationToken cancellationToken);
    
    void AddItem(TEntity item);
    void UpdateItem(TEntity item);
    void DeleteItem(TEntity item); 
}