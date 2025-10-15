using MainService.AL.Features.UserFlashCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserFlashCards;

[Tags("Flashcards")]
[Route("flashcards")]
[ApiController]
public class UserFlashController : ControllerBase
{
    private readonly IUserFlashCardsService _flashCardsService;

    public UserFlashController(IUserFlashCardsService flashCardsService)
    {
        _flashCardsService = flashCardsService;
    }

    // GET: /flashcards/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUser(Guid userId, CancellationToken cancellationToken)
    {
        var sets = await _flashCardsService.GetAllByUserAsync(userId, cancellationToken);
        return Ok(sets);
    }

    // GET: /flashcards/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GetByIdAsync(id, cancellationToken);
        if (set == null) return NotFound();
        return Ok(set);
    }
    
    // POST: /flashcards/generate/{userId}
    [HttpPost("generate/{userId}")]
    public async Task<IActionResult> GenerateFromUserWords(Guid userId, [FromQuery] string? title, int count, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GenerateFromUserWordsAsync(userId, title, count, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = set.Id }, set);
    }

    // DELETE: /flashcards/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _flashCardsService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
