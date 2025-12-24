using MainService.AL.Features.UserWords.DTO.Request;
using MainService.AL.Features.UserWords.Services;
using MainService.PL.Extensions;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserWord
{
    [Authorize(Roles = "User")]
    [Tags("UserWords")]
    [Route("user-words")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordService _userWordService;
        public UserWordsController(IUserWordService service)
        {
            _userWordService = service;
        }
        
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateParameters(nameof(pageIndex), nameof(pageCount))]
        public async Task<IActionResult> GetUserWords(int pageIndex,int pageCount,  CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var words = await _userWordService.GetAllWithUserIdAsync(userId,pageIndex, pageCount, cancellationToken);
            return Ok(words);
        }
        
        [HttpGet("{wordId}")]
        [ValidateParameters(nameof(wordId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserWord(Guid wordId, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            
            var userWord = await _userWordService.GetByIdsAsync(userId, wordId, cancellationToken);
            return Ok(userWord);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserWord([FromBody] RequestUserWordDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var created = await _userWordService.CreateAsync(dto, userId, cancellationToken);
            return Ok(created);
        }
        
        [HttpDelete("{wordId}")]
        [ValidateParameters(nameof(wordId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task DeleteUserWord(Guid wordId, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            
            await _userWordService.DeleteAsync(userId, wordId, cancellationToken);
        }
    }
}
