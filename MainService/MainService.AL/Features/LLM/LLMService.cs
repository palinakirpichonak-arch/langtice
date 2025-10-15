using MainService.BLL.Services.LLM;
using MainService.BLL.Services.UnitOfWork;

namespace MainService.AL.Features.LLM;

public class LLMService : ILLMService
{
    private readonly ILlmClient _llmClient;
    private readonly IUnitOfWork _unitOfWork;
    
    public LLMService(ILlmClient llmClient, IUnitOfWork unitOfWork)
    {
        _llmClient = llmClient;
        _unitOfWork = unitOfWork;
    }
    public async Task<string> ProcessPromptAsync(string prompt)
    {
        
        return await _llmClient.SendRequestAsync(prompt);
    }
}