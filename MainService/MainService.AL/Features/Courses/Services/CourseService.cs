using MainService.AL.Features.Courses.DTO;
using MainService.BLL.Data.Courses;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Courses.Services;

public class CourseService
    : Service<Course, CourseDto, Guid>, ICourseService
{
    public CourseService(ICourseRepository repository) : base(repository)
    {
    }

    public async Task ChangeStatusAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _repository.GetItemByIdAsync(id, cancellationToken);
        course.Status = course.Status!;
        await _repository.UpdateItemAsync(course, cancellationToken);
    }
} 