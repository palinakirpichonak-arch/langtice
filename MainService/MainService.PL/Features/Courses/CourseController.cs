using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Courses
{
    [Tags("Courses")]
    [Route("courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(id, cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCoursesById(CancellationToken cancellationToken)
        {
            var course = await _courseService.GetActiveCourses(cancellationToken);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCourses(CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllItemsAdminAsync(cancellationToken);
            return Ok(courses);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] RequestCourseDto dto, CancellationToken cancellationToken)
        {
            var course = await _courseService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course); //TODO: fix
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] RequestCourseDto dto, CancellationToken cancellationToken)
        {
            var updatedCourse = await _courseService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updatedCourse);
        }
    }
}
