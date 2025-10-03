using MainService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Abstractions;

public abstract class Repository<TEntity, TKey> : 
        IRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
{
    private readonly PostgreDbContext _dbContext;

    protected Repository(PostgreDbContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public async Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllItemsAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task AddItemAsync(TEntity item, CancellationToken cancellationToken)
    {
      await _dbContext.Set<TEntity>().AddAsync(item, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItemAsync(TEntity item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}