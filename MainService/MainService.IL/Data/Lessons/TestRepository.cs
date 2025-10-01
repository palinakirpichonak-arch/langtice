using MainService.DAL.Abstractions;
using MainService.DAL.Context;

namespace MainService.BLL.Data.Lessons;

public class TestRepository : MongoRepository<Test>, ITestRepository
{
    public TestRepository(MongoLangticeContext dbContext) : base(dbContext)
    {
    }
}