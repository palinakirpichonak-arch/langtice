using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.AL.Features.Words.Services;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Words
{
    [Authorize]
    [Tags("Words")]
    [Route("words")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ValidateParameters(nameof(pageIndex), nameof(pageCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllWords(int pageIndex, int pageCount, CancellationToken cancellationToken)
        {
            var words = await _wordService.GetAllAsync(pageIndex,pageCount, cancellationToken);
            return Ok(words);
        }
        
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        [ValidateParameters(nameof(id))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWordById(Guid id, CancellationToken cancellationToken)
        {
            var word = await _wordService.GetByIdAsync(id, cancellationToken);
            return Ok(word);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWord([FromBody] RequestWordDto dto, CancellationToken cancellationToken)
        {
            var created = await _wordService.CreateAsync(dto, cancellationToken);
            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ValidateParameters(nameof(id))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task DeleteWord(Guid id, CancellationToken cancellationToken)
        {
            await _wordService.DeleteAsync(id, cancellationToken);
        }
        
        [Authorize(Roles = "User, Admin")]
        [HttpPost("translate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> TranslateWord([FromBody] RequestTranslateWordDto dto, CancellationToken cancellationToken)
        {
            var result = await _wordService.TranslateWordAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
