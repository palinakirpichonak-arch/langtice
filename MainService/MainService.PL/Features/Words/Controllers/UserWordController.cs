using MainService.AL.Features.Words.DTO;
using MainService.AL.Words.Interfaces;
using MainService.DAL.Features.Words.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MainService.PL.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _service;

        public UserWordsController(IUserWordService service)
        {
            _service = service;
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
            if (dto == null) return BadRequest();

            var entity = dto.ToEntity();
            await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetUserWord), new { userId = entity.UserId, wordId = entity.WordId }, entity);
        }

        // DELETE api/userwords/{userId}/{wordId}
        [HttpDelete("{userId}/{wordId}")]
        public async Task<IActionResult> DeleteUserWord(Guid userId, Guid wordId, CancellationToken cancellationToken)
        {
            UserWordKey key = new UserWordKey { UserId = userId, WordId = wordId };
            await _service.DeleteAsync(key, cancellationToken);
            return NoContent();
        }
    }
}
