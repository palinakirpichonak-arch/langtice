using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class TranslationRepository : Repository<Translation,Guid>, ITranslationRepository
{
    private readonly LangticeContext _dbContext;

    public TranslationRepository(LangticeContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId, Guid courseId, 
                                                        CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Translation>()
                .Where(t => t.FromWordId == wordId && t.CourseId == courseId)
                .SingleAsync(cancellationToken);
    }
}