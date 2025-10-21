using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;
using MainService.DAL.Features.Test;

namespace MainService.DAL.Data.Tests;

public class TestRepository : MongoRepository<Test, string>, ITestRepository
{
    public TestRepository(MongoDbContext dbContext) : base(dbContext, MongoDbCollections.TestsCollectionName)
    {
    }
}