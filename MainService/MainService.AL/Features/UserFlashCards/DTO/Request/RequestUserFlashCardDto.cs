using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.UserFlashCards.DTO.Request;

public class RequestUserFlashCardDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string? Title  {get; set;} 
    
    [Required(ErrorMessage = "Count is required")]
    [Range(1,50, ErrorMessage = "Count must be between 1 and 50")]
    public int Count {get; set;}
}