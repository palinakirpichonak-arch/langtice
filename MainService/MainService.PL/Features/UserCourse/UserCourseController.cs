using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.Service;
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
    public async Task<IActionResult> GetAllUserCourses(Guid userId, CancellationToken cancellationToken)
    {
        if(userId == Guid.Empty)
            return BadRequest();
        var courses = await _userCourseService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{userId}/{courseId}")]
    public async Task<IActionResult> GetUserCourseByIds(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var course = await _userCourseService.GetByIdsAsync(userId, courseId, cancellationToken);
        if (course == null) return NotFound();
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserCourse([FromBody] RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _userCourseService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetUserCourseByIds), 
            new { userId = entity.UserId, courseId = entity.CourseId  }, entity);
    }

    [HttpDelete("{userId}/{courseId}")]
    public async Task<IActionResult> DeleteUserCourse(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        await _userCourseService.DeleteAsync( userId, courseId, cancellationToken);
        return NoContent();
    }
}
