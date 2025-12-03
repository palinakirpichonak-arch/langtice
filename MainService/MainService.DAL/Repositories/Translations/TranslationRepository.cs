using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.TranslationsModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Repositories.Translations;

public class TranslationRepository : Repository<Translation,Guid>, ITranslationRepository
{
    private readonly PostgreDbContext _dbContext;

    public TranslationRepository(PostgreDbContext dbContext) :  base(dbContext)
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