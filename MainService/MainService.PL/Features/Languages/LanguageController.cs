using MainService.AL.Features.Languages.DTO.Request;
using MainService.AL.Features.Languages.Services;
using MainService.PL.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Languages
{
    [Tags("Languages")]
    [ApiController]
    [Route("languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        [ValidateParameters(nameof(pageIndex), nameof(pageSize))]
        public async Task<IActionResult> GetAllLanguages(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var languages = await _languageService.GetAllAsync(pageIndex, pageSize, cancellationToken);
            return Ok(languages);
        }

        [HttpGet("{id:guid}")]
        [ValidateParameters(nameof(id))]
        public async Task<IActionResult> GetLanguageById(Guid id, CancellationToken cancellationToken)
        {
            var language = await _languageService.GetByIdAsync(id, cancellationToken);
            return Ok(language);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] RequestLanguageDto dto, CancellationToken cancellationToken)
        {
            var created = await _languageService.CreateAsync(dto, cancellationToken);
            return Ok(created);
        }

        [HttpDelete("{id:guid}")]
        [ValidateParameters(nameof(id))]
        public async Task DeleteLanguage(Guid id, CancellationToken cancellationToken)
        {
            await _languageService.DeleteAsync(id, cancellationToken);
        }
    }
}
