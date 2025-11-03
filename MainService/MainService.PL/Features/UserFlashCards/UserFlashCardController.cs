using MainService.AL.Features.UserFlashCards.DTO.Request;
using MainService.AL.Features.UserFlashCards.Services;
using MainService.PL.Extensions;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.UserFlashCards;

[Authorize(Roles = "User")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByUser(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        var sets = await _flashCardsService.GetAllByUserAsync(userId, cancellationToken);
        return Ok(sets);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var set = await _flashCardsService.GetByIdAsync(id, cancellationToken);
        return Ok(set);
    }
    
    [HttpPost("generate/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateFromUserWords([FromBody] RequestUserFlashCardDto dto, CancellationToken cancellationToken)
    {
        dto.UserId = User.GetUserId();
        
        var set = await _flashCardsService.GenerateFromUserWordsAsync(dto, cancellationToken);
        return Ok(set);
    }
    
    [HttpDelete("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task Delete(string id, CancellationToken cancellationToken)
    {
        await _flashCardsService.DeleteAsync(id, cancellationToken);
    }
}
