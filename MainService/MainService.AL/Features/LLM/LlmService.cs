using MainService.BLL.Services.LLM;
using MainService.BLL.Services.UnitOfWork;

namespace MainService.AL.Features.LLM;

public class LlmService : ILlmService
{
    private readonly ILlmClient _llmClient;
    private readonly IUnitOfWork _unitOfWork;
    
    public LlmService(ILlmClient llmClient, IUnitOfWork unitOfWork)
    {
        _llmClient = llmClient;
        _unitOfWork = unitOfWork;
    }
    public async Task<string> ProcessPromptAsync(string prompt, CancellationToken cancellationToken)
    {
        return await _llmClient.SendRequestAsync(prompt, cancellationToken);
    }
}