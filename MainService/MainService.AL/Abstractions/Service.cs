// IL/Services/BaseService.cs

using MainService.AL.Features.Abstractions;
using MainService.DAL;
using MainService.DAL.Abstractions;

namespace MainService.IL.Services;

public abstract class Service<TEntity, TKey> : IService<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
{
    protected readonly IRepository<TEntity, TKey> _repository;

    protected Service(IRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _repository.GetItemByIdAsync(id, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllItemsByAsync(cancellationToken);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.AddItemAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.UpdateItemAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}