using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Models.UserFlashCardModel;
using MongoDB.Driver;

namespace MainService.DAL.Configurations.MongoDb;

public class UserFlashCardsConfiguration : ICollectionConfiguration<UserFlashCard>
{
    public async Task<IMongoCollection<UserFlashCard>> InitializeAsync(IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<UserFlashCard>(MongoDbCollections.FlashCardsCollectionName);
        
        var indexModels = new List<CreateIndexModel<UserFlashCard>>
        {
            new(Builders<UserFlashCard>.IndexKeys.Ascending(x => x.CreatedAt),
                new CreateIndexOptions 
                { 
                    ExpireAfter = TimeSpan.FromDays(1)
                }),
        };
        
        await collection.Indexes.CreateManyAsync(indexModels,  cancellationToken);

        return collection;
    }
}