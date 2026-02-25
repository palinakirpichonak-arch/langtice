using System.ComponentModel.DataAnnotations;

namespace MainService.BLL.Options;

public class LlmOptions
{
    [Required]
    public string ApiKey { get; set; } = null!;
    [Required, Url]
    public string BaseUrl { get; set; } = null!;
    [Required]
    public string Model { get;set; } = null!;
}