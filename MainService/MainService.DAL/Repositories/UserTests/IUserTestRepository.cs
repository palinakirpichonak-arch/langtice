using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserTestModel;

namespace MainService.DAL.Repositories.UserTests;

public interface IUserTestRepository : IRepository<UserTest, Guid>
{
    
}