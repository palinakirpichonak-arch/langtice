using MongoDB.Driver;

namespace MainService.DAL.Abstractions;

public interface ICollectionConfiguration<TCollection>
{
    Task<IMongoCollection<TCollection>> InitializeAsync(IMongoDatabase database, CancellationToken cancellationToken);
}