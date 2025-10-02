using MainService.DAL.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace MainService.DAL.Abstractions;

public abstract class MongoRepository<T, TKey> : IMongoRepository<T, TKey>   where T : class
{
    private readonly MongoLangticeContext _dbContext;

    public MongoRepository(MongoLangticeContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        // await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (_dbContext.Set<T>().Contains(entity))
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        if (_dbContext.Set<T>().Contains(item))
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}