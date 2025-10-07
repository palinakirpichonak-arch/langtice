using MainService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Abstractions;

public abstract class Repository<TEntity, TKey> : 
        IRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
{
    protected readonly PostgreDbContext _dbContext;

    protected Repository(PostgreDbContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public virtual async Task<TEntity?> GetItemByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllItemsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<PaginatedList<TEntity>> GetAllItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Set<TEntity>()
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var count = await _dbContext.Set<TEntity>().CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedList<TEntity>(entities, pageIndex, totalPages);
    }

    public async Task<PaginatedList<TEntity>> GetAllItemsWithIdAsync(Guid id, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Set<TEntity>()
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).Where(e => e.Id.Equals(id))
            .ToListAsync(cancellationToken);

        var count = await _dbContext.Set<TEntity>().CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedList<TEntity>(entities, pageIndex, totalPages);
    }

    public virtual async Task AddItemAsync(TEntity item, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteItemAsync(TEntity item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}