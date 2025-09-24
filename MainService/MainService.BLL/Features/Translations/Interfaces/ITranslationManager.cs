using MainService.DAL.Models;

public interface ITranslationManager
{
    Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Translation translation, CancellationToken cancellationToken);
    Task DeleteAsync(Translation translation, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetForWordInCourseAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetForUserWordsAsync(IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken);
}