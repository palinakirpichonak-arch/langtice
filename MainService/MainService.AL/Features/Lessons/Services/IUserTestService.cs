using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.DTO.Response;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users.Models;

namespace MainService.AL.Features.Lessons.Services;

public interface IUserTestService
{
    Task<IEnumerable<UserTest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserTestDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedList<ResponseUserTestDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<PaginatedList<ResponseUserTestDto>> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseUserTestDto> CreateAsync(RequestUserTestDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}