using MainService.AL.Words.DTO;
using Microsoft.AspNetCore.Mvc;
namespace MainService.PL.Words.Controllers;

[Route("userwords")]
[ApiController]
public class UserWordsController : ControllerBase
{
    private readonly IUserWordService _service;

    public UserWordsController(IUserWordService service)
    {
        _service = service;
    }

    // Get a single UserWord
    [HttpGet("{userId}/{wordId}")]
    public async Task<IActionResult> GetUserWord(UserWordDTO userWordDto, CancellationToken cancellationToken)
    {
        var userWord = await _service.GetByIdsAsync(userWordDto, cancellationToken);
        if (userWord == null) return NotFound();
        return Ok(userWord);
    }

    // Get all UserWords for a user
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllUserWords(Guid userId, CancellationToken cancellationToken)
    {
        var userWords = await _service.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(userWords);
    }

    // Add a UserWord
    [HttpPost]
    public async Task<IActionResult> AddUserWord([FromBody] UserWordDTO? userWordDTO, CancellationToken cancellationToken)
    {
        if (userWordDTO == null) return BadRequest();
        
        await _service.AddUserWordAsync(userWordDTO, cancellationToken);
        return CreatedAtAction(nameof(GetUserWord),
            new { userId = userWordDTO.UserId, wordId = userWordDTO.WordId },
            userWordDTO);
    }

    // Delete a UserWord
    [HttpDelete("{userId}/{wordId}")]
    public async Task<IActionResult> DeleteUserWord(UserWordDTO userWordDTO, CancellationToken cancellationToken)
    {
        var userWord = await _service.GetByIdsAsync(userWordDTO, cancellationToken);
        if (userWord == null) return NotFound();

        await _service.DeleteUserWordAsync(userWordDTO, cancellationToken);
        return NoContent();
    }
}