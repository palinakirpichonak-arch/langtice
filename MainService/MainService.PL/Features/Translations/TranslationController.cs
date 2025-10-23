using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.Services;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Translations;

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
    [ValidateParameters(nameof(pageIndex), nameof(pageSize))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaginatedTranslations(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var translations = await _translationService.GetAllAsync(pageIndex, pageSize, cancellationToken);
        return Ok(translations);
    }
    
    [HttpGet("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTranslationById(Guid id, CancellationToken cancellationToken)
    {
        var translation = await _translationService.GetByIdAsync(id, cancellationToken);
        return Ok(translation);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTranslation([FromBody] RequestTranslationDto translation, CancellationToken cancellationToken)
    {
        var created = await _translationService.CreateAsync(translation, cancellationToken);
        return Ok(created);
    }
    
    [HttpDelete("{id}")]
    [ValidateParameters(nameof(id))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task DeleteTranslation(Guid id, CancellationToken cancellationToken)
    {
        await _translationService.DeleteAsync(id, cancellationToken);
    }
}
