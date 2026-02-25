using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.DTO.Response;

namespace MainService.AL.Features.UserCourse.Services;

public interface IUserCourseService 
{
    Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
}