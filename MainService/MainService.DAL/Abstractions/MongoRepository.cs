using MainService.DAL.Context.MongoDb;
using MongoDB.Driver;

namespace MainService.DAL.Abstractions;

public class MongoRepository<T, TKey> : IMongoRepository<T, TKey> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _collection.Find(_ => true).ToListAsync(cancellationToken);

    public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginatedList<T>> GetAllItemsWithIdAsync(
        TKey id, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", id); // фильтруем по самому Id
        var totalCount = (int)await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var items = await _collection.Find(filter)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, pageIndex, totalCount);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
        => await _collection.InsertOneAsync(entity, null, cancellationToken);

    public async Task UpdateAsync(TKey id, T entity, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions(), cancellationToken);
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
}