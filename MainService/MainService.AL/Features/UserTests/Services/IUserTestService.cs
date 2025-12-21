using MainService.AL.Features.UserTests.DTO.Request;
using MainService.AL.Features.UserTests.DTO.Response;
using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserTestModel;

namespace MainService.AL.Features.UserTests.Services;

public interface IUserTestService
{
    Task<IEnumerable<UserTest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserTestDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedList<ResponseUserTestDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PaginatedList<ResponseUserTestDto>> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseUserTestDto> CreateAsync(RequestUserTestDto dto, Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}