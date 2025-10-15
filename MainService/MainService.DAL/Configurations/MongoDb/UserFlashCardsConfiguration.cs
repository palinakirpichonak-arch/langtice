using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;
using MongoDB.Driver;

namespace MainService.DAL.Configurations.MongoDb;

public class UserFlashCardsConfiguration : ICollectionConfiguration<UserFlashCards>
{
    public IMongoCollection<UserFlashCards> Initialize(IMongoDatabase database)
    {
        var collection = database.GetCollection<UserFlashCards>("userflashcards");
        
        var indexModels = new List<CreateIndexModel<UserFlashCards>>
        {
            new(Builders<UserFlashCards>.IndexKeys.Ascending(x => x.CreatedAt),
                new CreateIndexOptions 
                { 
                    ExpireAfter = TimeSpan.FromMinutes(1)
                }),
        };
        
        collection.Indexes.CreateMany(indexModels);

        return collection;
    }
}