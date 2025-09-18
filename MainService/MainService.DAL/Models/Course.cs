namespace MainService.DAL.Models;

public class Course
{
    public Guid Id { get; set; }
    public Guid LearningLanguageId { get; set; }
    public Guid BaseLanguageId { get; set; }
    public virtual Language LearningLanguage { get; set; } = null!;
    public virtual Language BaseLanguage { get; set; } = null!;
    public bool? Status { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}