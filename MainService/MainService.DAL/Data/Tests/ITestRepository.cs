using MainService.DAL.Abstractions;
using MainService.DAL.Features.Test;

namespace MainService.DAL.Data.Tests;

public interface ITestRepository : IMongoRepository<Test, string>;