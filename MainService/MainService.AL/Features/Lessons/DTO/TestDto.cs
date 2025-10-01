using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO;

public class TestDto : IMapper<Test>
{
    public string Title { get; set; } = null!;
    public List<Question> Questions { get; set; } = new();
    
    public Test ToEntity()
    {
        return new Test
        {
            Title = Title,
            Questions = new List<Question>(Questions)
        };
    }
    
    public void MapTo(Test entity)
    {
        entity.Title = this.Title;
        entity.Questions = new List<Question>(this.Questions);
    }
}