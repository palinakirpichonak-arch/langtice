using MainService.AL.Features.UserWords.DTO.Request;
using MainService.AL.Features.UserWords.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserWord
{
    [Tags("UserWords")]
    [Route("user-words")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _userWordService;
        public UserWordsController(IUserWordService service,  IMapper mapper)
        {
            _userWordService = service;
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWords(Guid userId, int pageIndex,int pageCount,  CancellationToken cancellationToken)
        {
            var words = await _userWordService.GetAllWithUserIdAsync(userId,pageIndex, pageCount, cancellationToken);
            return Ok(words);
        }

        [HttpGet("{userId}/{wordId}")]
        public async Task<IActionResult> GetUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            var userWord = await _userWordService.GetByIdsAsync(userId, wordId, cancellationToken);
            if (userWord == null) return NotFound();
            return Ok(userWord);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserWord([FromBody] RequestUserWordDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();
            var created = await _userWordService.CreateAsync(dto, cancellationToken);
            return Ok(created);
        }

        [HttpDelete("{userId}/{wordId}")]
        public async Task<IActionResult> DeleteUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            await _userWordService.DeleteAsync(userId, wordId, cancellationToken);
            return NoContent();
        }
    }
}
