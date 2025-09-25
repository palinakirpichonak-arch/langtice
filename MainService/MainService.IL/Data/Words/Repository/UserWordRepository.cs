using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.BLL.Data.Words.Repository;

public class UserWordRepository : Repository<UserWord, UserWordKey>, IUserWordRepository
{
    private readonly LangticeContext _dbContext;

    public UserWordRepository(LangticeContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }
}