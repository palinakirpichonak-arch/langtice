using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.UserTestModel;

namespace MainService.DAL.Repositories.UserTests;

public class UserTestRepository : Repository<UserTest, Guid>, IUserTestRepository
{
    public UserTestRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}