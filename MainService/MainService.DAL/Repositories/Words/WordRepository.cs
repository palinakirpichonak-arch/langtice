using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.WordsModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Repositories.Words;

public class WordRepository : Repository<Word, Guid>, IWordRepository
{
    private readonly PostgreDbContext _dbContext;

    public WordRepository(PostgreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Word?> GetItemByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Words
            .Include(w => w.Language)
            .FirstOrDefaultAsync(w => w.Id == id, ct);
    }
}