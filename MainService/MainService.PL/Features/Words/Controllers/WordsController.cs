using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Words.Controllers
{
    [Tags("Words")]
    [Route("words")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        public WordsController(IWordService wordService,  IMapper mapper)
        {
            _wordService = wordService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllWords(int pageIndex, int pageCount, CancellationToken cancellationToken)
        {
            var words = await _wordService.GetAllAsync(pageIndex,pageCount, cancellationToken);
            if (words == null) return NotFound();
            return Ok(words);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWordById(Guid id, CancellationToken cancellationToken)
        {
            var word = await _wordService.GetByIdAsync(id, cancellationToken);
            if (word == null) return NotFound();
            return Ok(word);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] RequestWordDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();
            
            var created = await _wordService.CreateAsync(dto, cancellationToken);
            
            return CreatedAtAction(nameof(GetWordById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(Guid id, CancellationToken cancellationToken)
        {
            await _wordService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
