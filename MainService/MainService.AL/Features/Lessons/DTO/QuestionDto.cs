using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO;

public class QuestionDto : IMapper<Question>
{
    public string QuestionNumber { get; set; } = null!;
    public string Sentence { get; set; } = null!;
    public string[] AnswerOptions { get; set; } = null!;
    
    public Question ToEntity()
    {
        return new Question
        {
            QuestionNumber = QuestionNumber,
            Sentence = Sentence,
            AnswerOptions = AnswerOptions,
        };
    }
    
    public void MapTo(Question entity)
    {
        entity.QuestionNumber = QuestionNumber;
        entity.Sentence = Sentence;
        entity.AnswerOptions = AnswerOptions;
    }
}