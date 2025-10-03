using MainService.AL.Features.Languages.DTO;
using MainService.AL.Features.Languages.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Languages.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var languages = await _languageService.GetAllAsync(cancellationToken);
            return Ok(languages);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var language = await _languageService.GetByIdAsync(id, cancellationToken);
            if (language == null)
                return NotFound();

            return Ok(language);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LanguageDto dto, CancellationToken cancellationToken)
        {
            var created = await _languageService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), created);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _languageService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
