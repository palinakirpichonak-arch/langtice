using MainService.DAL.Abstractions;

namespace MainService.BLL.Data.Lessons;

public interface ITestRepository : IMongoRepository<Test>
{
}