using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Lessons;

namespace MainService.BLL.Data.Courses;

public class TestRepository : MongoRepository<Test, string>, ITestRepository
{
    public TestRepository(MongoDbContext dbContext) : base(dbContext, "tests")
    {
    }
}