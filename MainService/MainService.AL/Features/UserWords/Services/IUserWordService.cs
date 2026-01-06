using MainService.AL.Features.UserWords.DTO.Request;
using MainService.AL.Features.UserWords.DTO.Response;

namespace MainService.AL.Features.UserWords.Services;

public interface IUserWordService
{
    Task<ResponseUserWordDto> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
    Task<ResponseUserWordDto> CreateAsync(RequestUserWordDto dto, Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
}