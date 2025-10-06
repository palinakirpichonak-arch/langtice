using MainService.AL.Features.Words.DTO;
using MainService.AL.Features.Words.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace MainService.PL.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _userWordservice;
        public UserWordsController(IUserWordService service,  IMapper mapper)
        {
            _userWordservice = service;
        }

        // GET api/userwords/{userId}/{wordId}
        [HttpGet("{userId}/{wordId}")]
        public async Task<IActionResult> GetUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            var userWord = await _userWordservice.GetByIdsAsync(userId, wordId, cancellationToken);
            if (userWord == null) return NotFound();
            return Ok(userWord);
        }

        // GET api/translations/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWords(Guid userId, CancellationToken cancellationToken)
        {
            var words = await _userWordservice.GetAllByUserIdAsync(userId, cancellationToken);
            return Ok(words);
        }

        // POST api/userwords
        [HttpPost]
        public async Task<IActionResult> AddUserWord([FromBody] RequestUserWordDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();
            var created = await _userWordservice.CreateAsync(dto, cancellationToken);
            return Ok(created);
        }

        // DELETE api/userwords/{userId}/{wordId}
        [HttpDelete("{userId}/{wordId}")]
        public async Task<IActionResult> DeleteUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            await _userWordservice.DeleteAsync(userId, wordId, cancellationToken);
            return NoContent();
        }
    }
}
