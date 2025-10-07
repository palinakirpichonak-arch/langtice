using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Translations.Controllers;

[Tags("Translations")]
[Route("translations")]
[ApiController]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationService _translationService;

    public TranslationsController(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedTranslations(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var translations = await _translationService.GetAllAsync(pageIndex, pageSize, cancellationToken);
        return Ok(translations);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTranslationById(Guid id, CancellationToken cancellationToken)
    {
        var translation = await _translationService.GetByIdAsync(id, cancellationToken);
        if (translation == null)
            return NotFound();

        return Ok(translation);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTranslation([FromBody] RequestTranslationDto translation, CancellationToken cancellationToken)
    {
        if (translation == null) return BadRequest();

        var created = await _translationService.CreateAsync(translation, cancellationToken);
        return CreatedAtAction(nameof(GetTranslationById), new { id = created.Id }, created);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTranslation(Guid id, CancellationToken cancellationToken)
    {
        await _translationService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
