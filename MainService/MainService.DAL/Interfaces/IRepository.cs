namespace MainService.DAL;

public interface IRepository<T> where T : class
{
    Task<T?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllItemsByAsync(CancellationToken cancellationToken);
    Task AddItemAsync(T item, CancellationToken cancellationToken);
    Task UpdateItemAsync(T item, CancellationToken cancellationToken);
    Task DeleteItemAsync(T item, CancellationToken cancellationToken); 
}