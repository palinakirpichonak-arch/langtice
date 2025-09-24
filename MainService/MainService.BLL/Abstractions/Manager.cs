using MainService.BLL.Abstractions;
using MainService.DAL;

public class Manager<TEntity, TKey> : IManager<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly IRepository<TEntity, TKey> _repository;

    public Manager(IRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken) =>
        await _repository.GetItemByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        await _repository.GetAllItemsByAsync(cancellationToken);

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.AddItemAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.UpdateItemAsync(entity, cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken) =>
        await _repository.DeleteItemAsync(entity, cancellationToken);
}