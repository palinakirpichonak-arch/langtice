using MainService.BLL.Services.Options;
using Microsoft.Extensions.Options;

namespace MainService.BLL.Services.LLM;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class Llm : ILlmClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<LlmOptions> _llmOptions;

    public Llm(HttpClient httpClient, IOptions<LlmOptions> llmOptions)
    {
        _httpClient = httpClient;
        _llmOptions = llmOptions;
    }

    public async Task<string> SendRequestAsync(string prompt, CancellationToken cancellationToken)
    {
        var requestBody = new
        {
            model = _llmOptions.Value.Model,
            messages = new[]
            {
                new {
                    role = "user",
                    content = new[]
                    {
                        new { type = "text", text = prompt }
                    }
                }
            }
        };
        
        var json = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, _llmOptions.Value.BaseUrl)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        
        request.Headers.Add("Authorization", $"Bearer {_llmOptions.Value.ApiKey}");
        
        var response = await _httpClient.SendAsync(request, cancellationToken);
        
        // using var doc = JsonDocument.Parse(result);
        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            // Return a meaningful error message instead of crashing
            return $"Error {response.StatusCode}: {result}";
        }

        using var doc = JsonDocument.Parse(result);

        // Defensive parsing: make sure the expected path exists
        if (doc.RootElement.TryGetProperty("choices", out var choicesElement) &&
            choicesElement.ValueKind == JsonValueKind.Array &&
            choicesElement.GetArrayLength() > 0)
        {
            var content = choicesElement[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return content ?? "No content found in response.";
        }

        // Handle API error format
        if (doc.RootElement.TryGetProperty("error", out var errorElement))
        {
            var message = errorElement.GetProperty("message").GetString();
            return $"API Error: {message}";
        }

        return "Unexpected response format.";
    }
}
