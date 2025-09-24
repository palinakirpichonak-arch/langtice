using MainService.AL.Translations.DTO;
using MainService.DAL.Models;
using MainService.AL.Translations.Interfaces;

namespace MainService.BLL.Translations.Service;

public class TranslationService : ITranslationService
{
    private readonly ITranslationManager _manager;

    public TranslationService(ITranslationManager manager)
    {
        _manager = manager;
    }

    public async Task<Translation?> GetByIdAsync(Guid wordId, CancellationToken cancellationToken)
    {
        return await _manager.GetByIdAsync(wordId, cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _manager.GetAllAsync(cancellationToken);
    }

    public async Task<Translation> AddAsync(CreateTranslationDTO dto, CancellationToken cancellationToken)
    {
        var translation = new Translation
        {
            FromWordId = dto.FromWordId,
            ToWordId = dto.ToWordId,
            CourseId = dto.CourseId
        };

        await _manager.AddAsync(translation, cancellationToken);
        return translation;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _manager.GetByIdAsync(id, cancellationToken);
        if (entity != null)
            await _manager.DeleteAsync(entity, cancellationToken);
    }
    
    public Task<IEnumerable<Translation>> GetForWordInCourseAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken)
        => _manager.GetForWordInCourseAsync(fromWordId, courseId, cancellationToken);

    public Task<IEnumerable<Translation>> GetForUserWordsAsync(IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken)
        => _manager.GetForUserWordsAsync(wordIds, courseId, cancellationToken);
}