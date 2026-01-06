using MainService.AL.Features.Languages.DTO.Request;
using MainService.AL.Features.Languages.DTO.Response;

namespace MainService.AL.Features.Languages.Services;

public interface ILanguageService
{
    Task<ResponseLanguageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseLanguageDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseLanguageDto> CreateAsync(RequestLanguageDto dto, CancellationToken cancellationToken);
    Task<ResponseLanguageDto> UpdateAsync(Guid id, ResponseLanguageDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}