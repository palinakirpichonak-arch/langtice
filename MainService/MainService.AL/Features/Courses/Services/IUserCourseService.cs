using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;

namespace MainService.AL.Features.Courses.Services;

public interface IUserCourseService 
{
    Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
}