using MainService.DAL.Abstractions;
using MainService.DAL.Models.TestModel;

namespace MainService.DAL.Repositories.Tests;

public interface ITestRepository : IMongoRepository<Test, string>;