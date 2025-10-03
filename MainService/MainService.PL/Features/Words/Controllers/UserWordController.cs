using MainService.AL.Features.Words.DTO;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace MainService.PL.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _service;
        private IMapper _mapper;
        public UserWordsController(IUserWordService service,  IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/userwords/{userId}/{wordId}
        [HttpGet("{userId}/{wordId}")]
        public async Task<IActionResult> GetUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            var userWord = await _service.GetByIdsAsync(userId, wordId, cancellationToken);
            if (userWord == null) return NotFound();
            return Ok(userWord);
        }

        // GET api/translations/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWords(Guid userId, CancellationToken cancellationToken)
        {
            var words = await _service.GetAllByUserIdAsync(userId, cancellationToken);
            return Ok(words);
        }

        // POST api/userwords
        [HttpPost]
        public async Task<IActionResult> AddUserWord([FromBody] UserWordDTO dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        // DELETE api/userwords/{userId}/{wordId}
        [HttpDelete("{userId}/{wordId}")]
        public async Task<IActionResult> DeleteUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
