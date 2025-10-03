using MainService.AL.Features.Words.DTO;
using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Words.Interfaces;

public interface IWordService 
{
    Task<ResponseWordDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseWordDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseWordDto> CreateAsync(RequestWordDto dto, CancellationToken cancellationToken);
    Task<ResponseWordDto> UpdateAsync(Guid id, RequestWordDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}