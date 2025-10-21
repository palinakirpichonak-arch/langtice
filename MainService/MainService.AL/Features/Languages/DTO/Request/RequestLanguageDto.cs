using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Languages.DTO.Request
{
    public class RequestLanguageDto 
    {
        [Required(ErrorMessage = "Language name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Language name must be between 2 and 50 characters")]
        public string Name { get; set; } = null!;
    }
}