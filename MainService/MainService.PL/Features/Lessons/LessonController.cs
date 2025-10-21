using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.Services;
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
        
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetLessonsByCourseId(Guid courseId, int pageIndex, int pageCount, CancellationToken cancellationToken)
        {
            var lessons = await _lessonService.GetAllWithCourseIdAsync(courseId, pageIndex, pageCount, cancellationToken);
            return Ok(lessons);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(Guid id, CancellationToken cancellationToken)
        {
            var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();

            var created = await _lessonService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetLessonById), new { id = created.Id}, created);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();

            var updated = await _lessonService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id, CancellationToken cancellationToken)
        {
            await _lessonService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
