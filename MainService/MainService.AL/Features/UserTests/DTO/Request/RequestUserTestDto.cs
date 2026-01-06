using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.UserTests.DTO.Request;

public class RequestUserTestDto
{ 
    [Required(ErrorMessage = "UserTest name is required")]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "UserTest count is required")]
    [Range(1, 50, ErrorMessage = "Count must be between 1 and 25")]
    public int Count { get; set; } = 2;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
}