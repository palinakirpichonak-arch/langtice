namespace MainService.AL.Features.Llm;

public interface ILlmService
{
    Task<string> ProcessPromptAsync(string prompt, CancellationToken cancellationToken);
}