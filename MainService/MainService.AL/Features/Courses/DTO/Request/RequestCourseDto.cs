using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Courses.DTO.Request
{
    public class RequestCourseDto
    {
        [Required(ErrorMessage = "LearningLanguageId is required")]
        public Guid LearningLanguageId { get; set; }
        
        [Required(ErrorMessage = "BaseLanguageId is required")]
        public Guid BaseLanguageId { get; set; }
        public bool Status { get; set; }
    }
}