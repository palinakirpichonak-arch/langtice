using MainService.AL.Features.UserFlashCards.DTO.Request;
using MainService.AL.Features.UserFlashCards.Services;
using MainService.PL.Filters;
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
    [ValidateParameters(nameof(userId))]
    public async Task<IActionResult> GetAllByUser(Guid userId, CancellationToken cancellationToken)
    {
        var sets = await _flashCardsService.GetAllByUserAsync(userId, cancellationToken);
        return Ok(sets);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GetByIdAsync(id, cancellationToken);
        return Ok(set);
    }
    
    [HttpPost("generate/{userId}")]
    public async Task<IActionResult> GenerateFromUserWords([FromBody] RequestUserFlashCardDto dto, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GenerateFromUserWordsAsync(dto, cancellationToken);
        return Ok(set);
    }
    
    [HttpDelete("{id}")]
    [ValidateParameters(nameof(id))]
    public async Task Delete(string id, CancellationToken cancellationToken)
    {
        await _flashCardsService.DeleteAsync(id, cancellationToken);
    }
}
