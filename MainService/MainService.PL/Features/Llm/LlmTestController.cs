using MainService.AL.Features.Llm;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features.Llm;

[ApiController]
[Route("api/llm")]
public class LlmTestController : ControllerBase
{
    private readonly ILlmService _llmService;

    public LlmTestController(ILlmService llmService)
    {
        _llmService = llmService;
    }

    [HttpPost("test")]
    public async Task<IActionResult> Test([FromBody] LlmTestRequest request, CancellationToken cancellationToken)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest(new { error = "Prompt is required" });

        var response = await _llmService.ProcessPromptAsync(request.Prompt, cancellationToken);
        return Ok(new {Output = response});
    }
}

public record LlmTestRequest(string Prompt);
