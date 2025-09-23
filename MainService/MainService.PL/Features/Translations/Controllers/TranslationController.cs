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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var translation = await _service.GetByIdAsync(id, cancellationToken);
        if (translation == null) return NotFound();
        return Ok(translation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var translations = await _service.GetAllAsync(cancellationToken);
        return Ok(translations);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTranslationDTO dto, CancellationToken cancellationToken)
    {
        if (dto == null) return BadRequest();
        var translation = await _service.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = translation.Id }, translation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}