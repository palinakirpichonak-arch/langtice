using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations;

namespace MainService.DAL.Data.Translations;

public interface ITranslationRepository : IRepository<Translation, Guid>
{
    public Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId,Guid courseId, CancellationToken cancellationToken);
}