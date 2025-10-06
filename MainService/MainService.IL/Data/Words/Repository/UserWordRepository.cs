using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Words.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Words.Repository;

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

    public override async Task<UserWord?> GetItemByIdAsync(UserWordKey id, CancellationToken ct)
    {
        return await _dbContext.UserWords
            .Include(uw => uw.Word)
            .FirstOrDefaultAsync(uw => uw.UserId == id.UserId && uw.WordId == id.WordId, ct);
    }
    
}