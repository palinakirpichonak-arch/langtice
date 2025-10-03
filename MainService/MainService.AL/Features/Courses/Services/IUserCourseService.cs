using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Courses.Services;

public interface IUserCourseService 
{
    Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto?> GetByIdAsync(UserCourseKey id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseUserCourseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseUserCourseDto> CreateAsync(UserCourseDto dto, CancellationToken cancellationToken);
    Task<ResponseUserCourseDto> UpdateAsync(UserCourseKey id, UserCourseDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(UserCourseKey id, CancellationToken cancellationToken);
}