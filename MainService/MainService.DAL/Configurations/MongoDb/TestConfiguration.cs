using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Features.Test;
using MongoDB.Driver;

namespace MainService.DAL.Configurations.MongoDb;

public class TestConfiguration :  ICollectionConfiguration<Test>
{
    public async Task<IMongoCollection<Test>> InitializeAsync(IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<Test>(MongoDbCollections.TestsCollectionName);

       
        var indexKeys = Builders<Test>.IndexKeys.Ascending(t => t.Title);
        var indexModel = new CreateIndexModel<Test>(indexKeys, new CreateIndexOptions
        {
            Name = "idx_title"
        });
        
        await collection.Indexes.CreateOneAsync(indexModel, null, cancellationToken);

        return collection;
    }
}