using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.UserWords.DTO.Request;

public class RequestUserWordDto 
{
    [Required(ErrorMessage = "UserId is required")]
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "WordId is required")]
    public Guid WordId { get; set; }
}