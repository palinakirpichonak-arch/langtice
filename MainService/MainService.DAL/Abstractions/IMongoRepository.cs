namespace MainService.DAL.Abstractions
{
    public interface IMongoRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<PaginatedList<T>> GetAllItemsWithIdAsync(TKey id,int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(TKey id, T entity, CancellationToken cancellationToken);
        Task DeleteAsync(TKey id, CancellationToken cancellationToken);
    }
}