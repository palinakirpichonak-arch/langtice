using MainService.AL.Features.Translations.Services;
using Microsoft.AspNetCore.Mvc;

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
}
