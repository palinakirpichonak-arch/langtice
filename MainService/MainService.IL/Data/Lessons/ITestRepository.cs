using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MongoDB.Bson;

namespace MainService.BLL.Data.Lessons;

public interface ITestRepository : IMongoRepository<Test, string>
{
}