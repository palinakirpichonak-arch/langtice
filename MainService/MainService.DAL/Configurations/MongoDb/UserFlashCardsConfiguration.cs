using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Features.UserFlashCard;
using MongoDB.Driver;

namespace MainService.DAL.Configurations.MongoDb;

public class UserFlashCardsConfiguration : ICollectionConfiguration<UserFlashCards>
{
    public async Task<IMongoCollection<UserFlashCards>> InitializeAsync(IMongoDatabase database, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<UserFlashCards>(MongoDbCollections.FlashCardsCollectionName);
        
        var indexModels = new List<CreateIndexModel<UserFlashCards>>
        {
            new(Builders<UserFlashCards>.IndexKeys.Ascending(x => x.CreatedAt),
                new CreateIndexOptions 
                { 
                    ExpireAfter = TimeSpan.FromDays(1)
                }),
        };
        
        await collection.Indexes.CreateManyAsync(indexModels,  cancellationToken);

        return collection;
    }
}