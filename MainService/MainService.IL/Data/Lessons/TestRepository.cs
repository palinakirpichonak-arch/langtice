using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Lessons;

public class TestRepository : MongoRepository<Test, string>, ITestRepository
{
    public TestRepository(MongoDbContext dbContext) : base(dbContext, "tests")
    {
    }
}