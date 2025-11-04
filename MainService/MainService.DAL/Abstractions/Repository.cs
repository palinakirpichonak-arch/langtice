using System.Linq.Expressions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.Courses;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Abstractions;

public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
{
    protected readonly PostgreDbContext _dbContext;

    protected Repository(PostgreDbContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? pageIndex = null,
        int? pageSize = null,
        bool tracking = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> query = tracking 
            ? _dbContext.Set<TEntity>() 
            : _dbContext.Set<TEntity>().AsNoTracking();
        
        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        
        if(filter != null)
            query = query.Where(filter);
        
        if(orderBy != null)
            query = orderBy(query);

        if (pageIndex.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value);
        }
        return await query.ToListAsync(cancellationToken);
    }

    public virtual void AddItem(TEntity item)
    {
        _dbContext.Set<TEntity>().Add(item);
    }

    public virtual void UpdateItem(TEntity item)
    {
        _dbContext.Set<TEntity>().Update(item);
    }

    public virtual void DeleteItem(TEntity item)
    {
        _dbContext.Set<TEntity>().Remove(item);
    }
}