using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Users.Models;

namespace MainService.BLL.Data.Users;

public class UserTestRepository : Repository<UserTest, Guid>, IUserTestRepository
{
    public UserTestRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}