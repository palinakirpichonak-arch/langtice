using MainService.AL.Features.Languages.DTO;

namespace MainService.AL.Features.Languages.Services;

public interface ILanguageService
{
    Task<LanguageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<LanguageDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<LanguageDto> CreateAsync(LanguageDto dto, CancellationToken cancellationToken);
    Task<LanguageDto> UpdateAsync(Guid id, LanguageDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}