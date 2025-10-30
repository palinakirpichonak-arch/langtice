using System.Security.Claims;
using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.Service;
using MainService.PL.Extensions;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserCourse;

[Authorize(Roles = "User")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUserCourses(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        var courses = await _userCourseService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(courses);
    }
    
    [HttpGet("{userId}/{courseId}")]
    [ValidateParameters(nameof(courseId))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserCourseByIds(Guid courseId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        var course = await _userCourseService.GetByIdsAsync(userId, courseId, cancellationToken);
        return Ok(course);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserCourse([FromBody] RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        dto.UserId = User.GetUserId();
        
        var entity = await _userCourseService.CreateAsync(dto, cancellationToken);
        return Ok(entity);
    }
    
    [HttpDelete("{userId}/{courseId}")]
    [ValidateParameters(nameof(courseId))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task DeleteUserCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        await _userCourseService.DeleteAsync(userId, courseId, cancellationToken);
    }
}
