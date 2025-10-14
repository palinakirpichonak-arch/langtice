namespace MainService.BLL.Services.LLM;

public interface ILlmClient
{
    Task<string> SendRequestAsync(string prompt);
}