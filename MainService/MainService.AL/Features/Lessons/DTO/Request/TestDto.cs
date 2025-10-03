using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;
using MongoDB.Bson;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class TestDto : IMapper<Test>
{
    public string Title { get; set; } = null!;
    public List<Question> Questions { get; set; } = new();
    
    public Test ToEntity()
    {
        return new Test
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = Title,
            Questions = new List<Question>(Questions)
        };
    }
    
    public void ToDto(Test entity)
    {
        entity.Title = this.Title;
        entity.Questions = new List<Question>(this.Questions);
    }
}