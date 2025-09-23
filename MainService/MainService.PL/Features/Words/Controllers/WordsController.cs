using MainService.AL.Words.DTO;
using MainService.AL.Words.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // Get a single word by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWordById(Guid id, CancellationToken cancellationToken)
        {
            var word = await _wordService.GetWordByIdAsync(id, cancellationToken);
            if (word == null)
                return NotFound();

            return Ok(word);
        }

        // Get all words
        [HttpGet]
        public async Task<IActionResult> GetAllWords(CancellationToken cancellationToken)
        {
            var words = await _wordService.GetAllWordsAsync(cancellationToken);
            return Ok(words);
        }

        // Add a word
        [HttpPost]
        public async Task<IActionResult> AddWord([FromBody] WordDTO word, CancellationToken cancellationToken)
        {
            if (word == null)
                return BadRequest();

            var addedWord = await _wordService.AddWordAsync(word, cancellationToken);
            return CreatedAtAction(nameof(GetWordById), new { id = addedWord.Id }, addedWord);
        }

        // Delete a word
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(Guid id, CancellationToken cancellationToken)
        {
            await _wordService.DeleteWordAsync(id, cancellationToken);
            return NoContent();
        }
    }
}