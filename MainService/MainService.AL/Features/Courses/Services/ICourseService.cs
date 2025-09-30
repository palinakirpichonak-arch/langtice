using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Courses.DTO;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;

namespace MainService.AL.Features.Courses.Services;

public interface ICourseService : IService<Course,CourseDto, Guid>
{
    Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken);
}