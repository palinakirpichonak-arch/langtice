using MainService.BLL.Services.LLM;
using MainService.BLL.Services.UnitOfWork;

namespace MainService.AL.Features.LLM;

public class LlmService : ILlmService
{
    private readonly ILlmClient _llmClient;

    public LlmService(ILlmClient llmClient)
    {
        _llmClient = llmClient;
    }
    
    public async Task<string> ProcessPromptAsync(string prompt, CancellationToken cancellationToken)
    {
        return await _llmClient.SendRequestAsync(prompt, cancellationToken);
    }
}
