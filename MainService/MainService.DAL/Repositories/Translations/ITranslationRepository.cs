using MainService.DAL.Abstractions;
using MainService.DAL.Models.TranslationsModel;

namespace MainService.DAL.Repositories.Translations;

public interface ITranslationRepository : IRepository<Translation, Guid>
{
    public Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId,Guid courseId, CancellationToken cancellationToken);
}