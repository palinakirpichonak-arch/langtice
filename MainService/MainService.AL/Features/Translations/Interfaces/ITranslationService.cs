using MainService.AL.Translations.DTO;
using MainService.DAL.Models;
namespace MainService.AL.Translations.Interfaces;


public interface ITranslationService
{
        Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Translation>> GetAllAsync(CancellationToken cancellationToken);
        Task<Translation> AddAsync(CreateTranslationDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        
        Task<IEnumerable<Translation>> GetForWordInCourseAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken);
        Task<IEnumerable<Translation>> GetForUserWordsAsync(IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken);
}