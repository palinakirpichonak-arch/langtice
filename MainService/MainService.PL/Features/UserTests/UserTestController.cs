using MainService.AL.Features.UserTests.DTO.Request;
using MainService.AL.Features.UserTests.Services;
using MainService.PL.Filters;
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
        [ValidateParameters(nameof(userId))]
        public async Task<IActionResult> GetAllByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            var tests = await _userTestService.GetAllByUserIdAsync(userId, cancellationToken);
            return Ok(tests);
        }
        
        [HttpGet("{id}")]
        [ValidateParameters(nameof(id))]
        public async Task<IActionResult> GetUserTestById(Guid id, CancellationToken cancellationToken)
        {
            var test = await _userTestService.GetByIdAsync(id, cancellationToken);
            return Ok(test);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserTest([FromBody] RequestUserTestDto dto, CancellationToken cancellationToken)
        {
            var createdTest = await _userTestService.CreateAsync(dto, cancellationToken);
            return Ok(createdTest);
        }
        
        [HttpDelete("{id}")]
        [ValidateParameters(nameof(id))]
        public async Task DeleteUserTest(Guid id, CancellationToken cancellationToken)
        {
            await _userTestService.DeleteAsync(id, cancellationToken);
        }
    }
}
