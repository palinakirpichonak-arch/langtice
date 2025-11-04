using System.ComponentModel.DataAnnotations;

namespace AuthService.AL.Features.Users.Dto;

public class LoginUserDto
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}