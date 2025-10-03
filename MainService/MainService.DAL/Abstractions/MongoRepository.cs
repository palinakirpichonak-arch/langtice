using MongoDB.Driver;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;

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