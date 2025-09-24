using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class UserWordRepository : Repository<UserWord, UserWordKey>, IUserWordRepository
{
    private readonly LangticeContext _dbContext;

    public UserWordRepository(LangticeContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }
}