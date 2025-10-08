using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.DAL.Abstractions;

namespace MainService.AL.Features.Translations.Services;

public interface ITranslationService 
{
    Task<ResponseTranslationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedList<ResponseTranslationDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseTranslationDto> CreateAsync(RequestTranslationDto dto, CancellationToken cancellationToken);
    Task<ResponseTranslationDto> UpdateAsync(Guid id, RequestTranslationDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}