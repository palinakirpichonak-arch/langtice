using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.UserWord;
using Microsoft.EntityFrameworkCore;
namespace MainService.DAL.Data.UserWord;

public class UserWordRepository : Repository<DAL.Features.UserWord.UserWord, UserWordKey>, IUserWordRepository
{
    private readonly PostgreDbContext _dbContext;

    public UserWordRepository(PostgreDbContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Features.UserWord.UserWord>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.UserWords.Include(uw => uw.Word).ToListAsync(ct);
    }

    public async Task<PaginatedList<DAL.Features.UserWord.UserWord>> GetAllByUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.UserWords
            .Include(uw => uw.Word)
            .Where(uw => uw.UserId == userId);

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PaginatedList<DAL.Features.UserWord.UserWord>(items, pageIndex, totalPages);
    }

    public async Task<DAL.Features.UserWord.UserWord?> GetItemByIdAsync(UserWordKey id, CancellationToken ct)
    {
        return await _dbContext.UserWords
            .Include(uw => uw.Word)
            .FirstOrDefaultAsync(uw => uw.UserId == id.UserId && uw.WordId == id.WordId, ct);
    }
    
}