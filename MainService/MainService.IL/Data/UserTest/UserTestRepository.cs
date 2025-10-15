using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;

namespace MainService.BLL.Data.UserTest;

public class UserTestRepository : Repository<DAL.Features.UserTest.UserTest, Guid>, IUserTestRepository
{
    public UserTestRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}