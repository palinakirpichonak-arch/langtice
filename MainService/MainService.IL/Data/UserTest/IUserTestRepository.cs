using MainService.DAL.Abstractions;

namespace MainService.BLL.Data.UserTest;

public interface IUserTestRepository : IRepository<DAL.Features.UserTest.UserTest, Guid>
{
    
}