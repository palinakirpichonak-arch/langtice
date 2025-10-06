using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        public WordsController(IWordService wordService,  IMapper mapper)
        {
            _wordService = wordService;
        }

        // GET words/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWordById(Guid id, CancellationToken cancellationToken)
        {
            var word = await _wordService.GetByIdAsync(id, cancellationToken);
            if (word == null) return NotFound();
            return Ok(word);
        }

        // POST words/
        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] RequestWordDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();
            
            var created = await _wordService.CreateAsync(dto, cancellationToken);
            
            return CreatedAtAction(nameof(GetWordById), new { id = created.Id }, created);
        }
        // DELETE words/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(Guid id, CancellationToken cancellationToken)
        {
            await _wordService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
