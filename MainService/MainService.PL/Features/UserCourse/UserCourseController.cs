using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.Service;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserCourse;

[Tags("UserCourses")]
[Route("user-courses")]
[ApiController]
public class UserCourseController : ControllerBase
{
    private readonly IUserCourseService _userCourseService;

    public UserCourseController(IUserCourseService userCourseService)
    {
        _userCourseService = userCourseService;
    }

    [HttpGet("user/{userId}")]
    [ValidateParameters(nameof(userId))]
    public async Task<IActionResult> GetAllUserCourses(Guid userId, CancellationToken cancellationToken)
    {
        var courses = await _userCourseService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{userId}/{courseId}")]
    [ValidateParameters(nameof(userId), nameof(courseId))]
    public async Task<IActionResult> GetUserCourseByIds(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var course = await _userCourseService.GetByIdsAsync(userId, courseId, cancellationToken);
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserCourse([FromBody] RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _userCourseService.CreateAsync(dto, cancellationToken);
        return Ok(entity);
    }

    [HttpDelete("{userId}/{courseId}")]
    [ValidateParameters(nameof(userId), nameof(courseId))]
    public async Task DeleteUserCourse(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        await _userCourseService.DeleteAsync(userId, courseId, cancellationToken);
    }
}
