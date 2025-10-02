using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Courses.DTO;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Courses.Services;

public interface IUserCourseService : IService<UserCourse, UserCourseDto, UserCourseKey>
{
    Task<IEnumerable<UserCourse>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}