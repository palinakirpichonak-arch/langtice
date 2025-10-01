using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO;

public class LessonDto : IMapper<Lesson>
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
    public string? TestId { get; set; }

    public Lesson ToEntity()
    {
        return new Lesson
        {
            Id = Guid.NewGuid(),
            CourseId = CourseId,
            Name = Name,
            Description = Description,
            OrderNum = OrderNum,
            TestId = TestId,
        };
    }

    public void MapTo(Lesson entity)
    {
        entity.CourseId = CourseId;
        entity.Name = Name;
        entity.Description = Description;
        entity.OrderNum = OrderNum;
        entity.TestId = TestId;
    }
}