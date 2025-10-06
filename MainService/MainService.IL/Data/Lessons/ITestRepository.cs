using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Lessons;

public interface ITestRepository : IMongoRepository<Test, string>;