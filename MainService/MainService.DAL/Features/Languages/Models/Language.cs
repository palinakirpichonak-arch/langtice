namespace MainService.DAL.Models;

public class Language
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Course> CoursesAsBase { get; set; } = new List<Course>();
    public ICollection<Course> CoursesAsLearning { get; set; } = new List<Course>();
    public ICollection<Word> Words { get; set; } = new List<Word>();
}