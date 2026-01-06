using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Models.TestModel;

namespace MainService.DAL.Repositories.Tests;

public class TestRepository : MongoRepository<Test, string>, ITestRepository
{
    public TestRepository(MongoDbContext dbContext) : base(dbContext, MongoDbCollections.TestsCollectionName)
    {
    }
}