using MainService.AL.Translations.DTO;
using Microsoft.AspNetCore.Mvc;
using MainService.AL.Translations.Interfaces;

namespace MainService.PL.Translations.Controllers;

[Route("[controller]")]
[ApiController]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationService _service;

    public TranslationsController(ITranslationService service)
    {
        _service = service;
    }
    
    [HttpGet("by-word/{fromWordId}/course/{courseId}")]
    public async Task<IActionResult> GetWordInCourse(Guid fromWordId, Guid courseId, CancellationToken cancellationToken)
    {
        var translations = await _service.GetForWordInCourseAsync(fromWordId, courseId, cancellationToken);
        return Ok(translations);
    }

    [HttpGet("by-user-words/{courseId}")]
    public async Task<IActionResult> GetForUserWords([FromBody] IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken)
    {
        var translations = await _service.GetForUserWordsAsync(wordIds, courseId, cancellationToken);
        return Ok(translations);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTranslation([FromBody] CreateTranslationDTO dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();
        var translation = await _service.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetWordInCourse), new { id = translation.Id }, translation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTranslation(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}