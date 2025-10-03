using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Courses.DTO.Response;

namespace MainService.AL.Features.Courses.Services;

public interface ICourseService 
{
    Task<ResponseCourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseCourseDto>> GetAllItemsAdminAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ResponseCourseDto>> GetActiveCourses(CancellationToken cancellationToken);
    Task<ResponseCourseDto> CreateAsync(CourseDto dto, CancellationToken cancellationToken);
    Task<ResponseCourseDto> UpdateAsync(Guid id, CourseDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}