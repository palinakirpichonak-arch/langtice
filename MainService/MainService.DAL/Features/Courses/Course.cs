using MainService.DAL.Abstractions;
using MainService.DAL.Features.Languages;
using MainService.DAL.Features.Lessons;

namespace MainService.DAL.Features.Courses;

public class Course  : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid LearningLanguageId { get; set; }
    public Guid BaseLanguageId { get; set; }
    public Language LearningLanguage { get; set; } = null!;
    public Language BaseLanguage { get; set; } = null!;
    public bool? Status { get; set; }
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<UserCourse.UserCourse> UserCourses { get; set; } = new List<UserCourse.UserCourse>();
}