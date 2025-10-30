using System.Linq.Expressions;
using MainService.DAL.Features.Courses;

namespace MainService.DAL.Abstractions;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{ 
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? pageIndex = null,
        int? pageSize = null,
        bool tracking = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[]? includes);
    void AddItem(TEntity item);
    void UpdateItem(TEntity item);
    void DeleteItem(TEntity item); 
}