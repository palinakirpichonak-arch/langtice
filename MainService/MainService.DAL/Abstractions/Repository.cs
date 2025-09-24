using Microsoft.EntityFrameworkCore;
namespace MainService.DAL;

public abstract class Repository<TEntity, TKey> : 
        IRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
{
    protected readonly LangticeContext _dbContext;

    protected Repository(LangticeContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public async Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FindAsync(new object[] {id}, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllItemsByAsync(CancellationToken cancellationToken)
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