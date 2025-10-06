using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Courses.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserCourseController : ControllerBase
{
    private readonly IUserCourseService _userCourseService;

    public UserCourseController(IUserCourseService userCourseService)
    {
        _userCourseService = userCourseService;
    }

    // GET: api/usercourse/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserCourses(Guid userId, CancellationToken cancellationToken)
    {
        if(userId == Guid.Empty)
            return BadRequest();
        var courses = await _userCourseService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(courses);
    }

    // GET: api/usercourse/{userId}/{courseId}
    [HttpGet("{userId}/{courseId}")]
    public async Task<IActionResult> GetUserCourse(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var course = await _userCourseService.GetByIdsAsync(userId, courseId, cancellationToken);
        if (course == null) return NotFound();
        return Ok(course);
    }

    // POST: api/usercourse
    [HttpPost]
    public async Task<IActionResult> AddUserCourse([FromBody] RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _userCourseService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetUserCourse), 
            new { userId = entity.UserId, courseId = entity.CourseId  }, entity);
    }

    // DELETE: api/usercourse/{userId}/{courseId}
    [HttpDelete("{userId}/{courseId}")]
    public async Task<IActionResult> DeleteUserCourse(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        await _userCourseService.DeleteAsync( userId, courseId, cancellationToken);
        return NoContent();
    }
}
