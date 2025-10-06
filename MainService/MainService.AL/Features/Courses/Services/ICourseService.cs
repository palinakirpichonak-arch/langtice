using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;

namespace MainService.AL.Features.Courses.Services;

public interface ICourseService 
{
    Task<ResponseCourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseCourseDto>> GetAllItemsAdminAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ResponseCourseDto>> GetActiveCourses(CancellationToken cancellationToken);
    Task<ResponseCourseDto> CreateAsync(RequestCourseDto dto, CancellationToken cancellationToken);
    Task<ResponseCourseDto> UpdateAsync(Guid id, RequestCourseDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}