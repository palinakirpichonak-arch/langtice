using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Courses.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Courses.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(id, cancellationToken);
            if (course == null) return NotFound();
            return Ok(course);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllAsync(cancellationToken);
            return Ok(courses);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest("Invalid course data.");

            var course = await _courseService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest("Invalid course data.");

            var updatedCourse = await _courseService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updatedCourse);
        }
        
        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, CancellationToken cancellationToken)
        {
            await _courseService.ChangeStatusAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
