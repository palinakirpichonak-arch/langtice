using MainService.DAL.Abstractions;
using MainService.DAL.Features.Test;
using MongoDB.Driver;

namespace MainService.DAL.Configurations.MongoDb;

public class TestConfiguration :  ICollectionConfiguration<Test>
{
    public IMongoCollection<Test> Initialize(IMongoDatabase database) 
        => database.GetCollection<Test>("tests");
}