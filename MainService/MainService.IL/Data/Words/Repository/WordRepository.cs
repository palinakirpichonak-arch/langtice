using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.BLL.Data.Words.Repository;

public class WordRepository : Repository<Word, Guid>, IWordRepository
{
    private readonly LangticeContext _dbContext;

    public WordRepository(LangticeContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}