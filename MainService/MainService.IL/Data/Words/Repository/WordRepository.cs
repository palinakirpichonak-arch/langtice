using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Words.Repository;

public class WordRepository : Repository<Word, Guid>, IWordRepository
{
    private readonly PostgreDbContext _dbContext;

    public WordRepository(PostgreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<Word?> GetItemByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Words
            .Include(w => w.Language)
            .FirstOrDefaultAsync(w => w.Id == id, ct);
    }
}