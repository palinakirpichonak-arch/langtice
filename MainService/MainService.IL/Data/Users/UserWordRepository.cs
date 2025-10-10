using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Words.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Users;

public class UserWordRepository : Repository<UserWord, UserWordKey>, IUserWordRepository
{
    private readonly PostgreDbContext _dbContext;

    public UserWordRepository(PostgreDbContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public override async Task<IEnumerable<UserWord>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.UserWords
            .Include(uw => uw.Word)
            .ToListAsync(ct);
    }

    public async Task<PaginatedList<UserWord>> GetAllByUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
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

        return new PaginatedList<UserWord>(items, pageIndex, totalPages);
    }

    public override async Task<UserWord?> GetItemByIdAsync(UserWordKey id, CancellationToken ct)
    {
        return await _dbContext.UserWords
            .Include(uw => uw.Word)
            .FirstOrDefaultAsync(uw => uw.UserId == id.UserId && uw.WordId == id.WordId, ct);
    }
    
}