using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Translations.DTO;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.DAL.Features.Translations.Models;

namespace MainService.AL.Features.Translations.Services;

public interface ITranslationService 
{
    Task<ResponseTranslationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseTranslationDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseTranslationDto> CreateAsync(TranslationDto dto, CancellationToken cancellationToken);
    Task<ResponseTranslationDto> UpdateAsync(Guid id, TranslationDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}