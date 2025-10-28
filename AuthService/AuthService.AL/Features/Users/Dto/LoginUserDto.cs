using System.ComponentModel.DataAnnotations;

namespace AuthService.AL.Features.Users.Dto;

public class LoginUserDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}