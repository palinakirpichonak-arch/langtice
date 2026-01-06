using System.ComponentModel.DataAnnotations;

namespace AuthService.AL.Features.Users.Dto;

public class RegisterUserDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool? Status { get; set; }
}