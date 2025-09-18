namespace MainService.DAL.Models;

public class LessonСontent
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public virtual Lesson Lesson { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int OrderNum { get; set; }
    public string MongoId { get; set; } = null!;
}
