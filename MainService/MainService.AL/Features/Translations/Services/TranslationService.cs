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

    public async Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _manager.GetByIdAsync(id, cancellationToken);
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
}