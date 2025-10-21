using MainService.AL.Features.UserFlashCards.DTO.Request;
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
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUser(Guid userId, CancellationToken cancellationToken)
    {
        var sets = await _flashCardsService.GetAllByUserAsync(userId, cancellationToken);
        return Ok(sets);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GetByIdAsync(id, cancellationToken);
        
        if (set == null)
        {
            return NotFound();
        }
        
        return Ok(set);
    }
    
    [HttpPost("generate/{userId}")]
    public async Task<IActionResult> GenerateFromUserWords([FromBody] RequestUserFlashCardDto dto, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GenerateFromUserWordsAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = set.Id }, set);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _flashCardsService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
