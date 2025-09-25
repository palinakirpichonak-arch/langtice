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
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;

        public WordsController(IWordService wordService)
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
        public async Task<IActionResult> CreateWord([FromBody] WordDto dto, CancellationToken cancellationToken)
        {
            if (dto == null) return BadRequest();
            
            await _wordService.CreateAsync(dto, cancellationToken);

            var word = dto.ToEntity();
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
