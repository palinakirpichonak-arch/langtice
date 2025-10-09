using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;

namespace MainService.BLL.Data.Courses;

public interface ITestRepository : IMongoRepository<Test, string>;