using MainService.AL.Features.Words.DTO;
using MainService.AL.Words.Interfaces;
using MainService.DAL.Features.Words.Models;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace MainService.PL.Words.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IMapper _mapper;
        public WordsController(IWordService wordService,  IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
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
            
            await _wordService.CreateAsync(dto, cancellationToken);

            var word = _mapper.Map<Word>(dto);
            return CreatedAtAction(nameof(GetWordById), new { id = word.Id }, word);
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
