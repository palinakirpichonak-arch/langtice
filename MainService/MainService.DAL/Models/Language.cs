namespace MainService.DAL.Models;

public class Language
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Course> CoursesAsBase { get; set; } = new List<Course>();
    public virtual ICollection<Course> CoursesAsLearning { get; set; } = new List<Course>();
    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}