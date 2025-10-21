using MainService.DAL.Abstractions;

namespace MainService.DAL.Data.UserTest;

public interface IUserTestRepository : IRepository<DAL.Features.UserTest.UserTest, Guid>
{
    
}