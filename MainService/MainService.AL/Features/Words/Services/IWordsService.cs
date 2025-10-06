using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;

namespace MainService.AL.Features.Words.Services;

public interface IWordService 
{
    Task<ResponseWordDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseWordDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseWordDto> CreateAsync(RequestWordDto dto, CancellationToken cancellationToken);
    Task<ResponseWordDto> UpdateAsync(Guid id, RequestWordDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}