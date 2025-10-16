using MongoDB.Driver;

namespace MainService.DAL.Abstractions;

public interface ICollectionConfiguration<TCollection>
{
    IMongoCollection<TCollection> Initialize(IMongoDatabase database);
}