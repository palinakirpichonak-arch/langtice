namespace MainService.AL.Features.LLM;

public interface ILLMService
{
    Task<string> ProcessPromptAsync(string prompt);
}