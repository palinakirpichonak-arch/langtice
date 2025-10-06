using MainService.AL.Features.Translations.DTO;
using MainService.AL.Features.Translations.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Translations.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationService _service;

    public TranslationsController(ITranslationService service)
    {
        _service = service;
    }

    // GET translations/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var translation = await _service.GetByIdAsync(id, cancellationToken);
        if (translation == null)
            return NotFound();

        return Ok(translation);
    }
    
    // POST translations
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestTranslationDto translation, CancellationToken cancellationToken)
    {
        if (translation == null) return BadRequest();

        var created = await _service.CreateAsync(translation, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    // DELETE translations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
