using MainService.DAL.Models;
namespace MainService.DAL.Words.Repository;

public class WordRepository : Repository<Word, Guid>, IWordRepository
{
    private readonly LangticeContext _dbContext;

    public WordRepository(LangticeContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}