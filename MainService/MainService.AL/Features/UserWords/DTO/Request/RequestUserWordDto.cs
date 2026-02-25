using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.UserWords.DTO.Request;

public class RequestUserWordDto 
{
    [Required(ErrorMessage = "WordId is required")]
    public Guid WordId { get; set; }
}