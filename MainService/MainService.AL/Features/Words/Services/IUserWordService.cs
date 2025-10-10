using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.DAL.Abstractions;

namespace MainService.AL.Features.Words.Services;

public interface IUserWordService
{
    Task<ResponseUserWordDto> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserWordDto> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
    Task<ResponseUserWordDto> CreateAsync(RequestUserWordDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
}