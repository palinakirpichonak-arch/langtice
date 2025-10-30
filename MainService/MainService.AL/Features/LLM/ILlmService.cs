namespace MainService.AL.Features.LLM;

public interface ILlmService
{
    Task<string> ProcessPromptAsync(string prompt, CancellationToken cancellationToken);
}