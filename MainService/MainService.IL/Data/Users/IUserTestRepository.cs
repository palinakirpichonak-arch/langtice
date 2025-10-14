using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users.Models;

namespace MainService.BLL.Data.Users;

public interface IUserTestRepository : IRepository<UserTest, Guid>
{
    
}