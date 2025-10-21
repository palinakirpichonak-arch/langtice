using System.ComponentModel.DataAnnotations;
using MainService.DAL.Features.Test;

namespace MainService.AL.Features.Tests.DTO.Request;

public class TestDto 
{
    [Required(ErrorMessage = "Test title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Test title must be between 3 and 100 characters")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Test questins are required")]
    public List<Question> Questions { get; set; } = new();
}