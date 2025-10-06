using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Lessons.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourseId(Guid courseId, CancellationToken cancellationToken)
        {
            var lessons = await _lessonService.GetAllByCourseIdAsync(courseId, cancellationToken);
            return Ok(lessons);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();

            var created = await _lessonService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id}, created);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RequestLessonDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();

            var updated = await _lessonService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _lessonService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
