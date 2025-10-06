namespace MainService.AL.Features.Courses.DTO.Request
{
    public class RequestCourseDto
    {
        public Guid LearningLanguageId { get; set; }
        public Guid BaseLanguageId { get; set; }
        public bool Status { get; set; }
    }
}