using MainService.DAL.Models;

namespace MainService.DAL.Words.Repository;

public interface ITranslationRepository : IRepository<Translation>
{
    Task<Translation> GetTranslationByWordIdAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetTranslationsForWordInCourseAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetTranslationsForUserWordsAsync(IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken);
}