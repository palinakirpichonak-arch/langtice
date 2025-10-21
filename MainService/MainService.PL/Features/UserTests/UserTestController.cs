using MainService.AL.Features.UserTests.DTO.Request;
using MainService.AL.Features.UserTests.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserTests
{
    [Tags("UserTests")]
    [Route("usertests")]
    [ApiController]
    public class UserTestController : ControllerBase
    {
        private readonly IUserTestService _userTestService;

        public UserTestController(IUserTestService userTestService)
        {
            _userTestService = userTestService;
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            var tests = await _userTestService.GetAllByUserIdAsync(userId, cancellationToken);
            return Ok(tests);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTestById(Guid id, CancellationToken cancellationToken)
        {
            var test = await _userTestService.GetByIdAsync(id, cancellationToken);
            
            if (test == null)
            {
                return NotFound();
            }
            
            return Ok(test);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserTest([FromBody] RequestUserTestDto dto, CancellationToken cancellationToken)
        {
            var createdTest = await _userTestService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetUserTestById), new { id = createdTest.Id }, createdTest);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTest(Guid id, CancellationToken cancellationToken)
        {
            await _userTestService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
