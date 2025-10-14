using MainService.AL.Features.LLM;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LLMController : ControllerBase
{
    private readonly ILLMService _illmService;

    public LLMController(ILLMService illmService)
    {
        _illmService = illmService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendPrompt([FromBody] string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            return BadRequest("Prompt cannot be empty.");

        var result = await _illmService.ProcessPromptAsync(prompt);
        return Ok(result);
    }
}