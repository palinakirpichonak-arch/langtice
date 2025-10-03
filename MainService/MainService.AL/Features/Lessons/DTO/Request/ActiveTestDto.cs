using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO;

public class ActiveTestDto : IMapper<Test>
{
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new();
    
    public Test ToEntity()
    {
        return new Test
        {
            Title = Title,
            Questions = Questions.Select(q => q.ToEntity()).ToList()
        };
    }
    
    public void ToDto(Test entity)
    {
        Title = entity.Title;
        Questions = entity.Questions.Select(q => new QuestionDto
        {
            QuestionNumber = q.QuestionNumber,
            Sentence = q.Sentence,
            AnswerOptions = q.AnswerOptions
        }).ToList();
    }
}