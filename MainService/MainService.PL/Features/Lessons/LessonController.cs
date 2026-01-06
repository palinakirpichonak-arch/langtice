using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.Services;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Lessons
{
    [Tags("Lessons")]
    [Route("lessons")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        
        [Authorize(Roles = "User, Admin")]
        [HttpGet("course/{courseId}")]
        [ValidateParameters(nameof(courseId), nameof(pageIndex), nameof(pageCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLessonsByCourseId(Guid courseId, int pageIndex, int pageCount, CancellationToken cancellationToken)
        {
            var lessons = await _lessonService.GetAllWithCourseIdAsync(courseId, pageIndex, pageCount, cancellationToken);
            return Ok(lessons);
        }
        
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        [ValidateParameters(nameof(id))]
        [ProducesResponseType(StatusCodes.Status200OK )]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLessonById(Guid id, CancellationToken cancellationToken)
        {
            var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
            return Ok(lesson);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateLesson([FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            var created = await _lessonService.CreateAsync(dto, cancellationToken);
            return Ok(created);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ValidateParameters(nameof(id))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            var updated = await _lessonService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ValidateParameters(nameof(id))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task DeleteLesson(Guid id, CancellationToken cancellationToken)
        {
            await _lessonService.DeleteAsync(id, cancellationToken);
        }
    }
}
