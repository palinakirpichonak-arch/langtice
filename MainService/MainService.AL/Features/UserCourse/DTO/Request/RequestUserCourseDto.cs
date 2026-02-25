using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.UserCourse.DTO.Request;

public class RequestUserCourseDto
{
    [Required(ErrorMessage = "UserId is required")]
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "CourseId is required")]
    public Guid CourseId { get; set; }
}