using MainService.AL.Abstractions;
using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using MainService.BLL.Data.Lessons;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MongoDB.Bson;

namespace MainService.AL.Features.Lessons.Services;

public class TestService : MongoService<Test, TestDto,string>, ITestService
{
    public TestService(ITestRepository repository) : base(repository)
    {
    }
    
    public async Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken)
    {
        var test = await _repository.GetByIdAsync(testId,cancellationToken);
        ActiveTestDto activeTest = new();
        activeTest.ToDto(test);
        
        return activeTest;
    }
    
    public async Task<(int correct, int mistake)> CheckTest(string testId, UserTestDto userTest, CancellationToken cancellationToken)
    {
        (int correct, int mistake) = (0, 0);
        var test = await _repository.GetByIdAsync(testId,cancellationToken);
        
        var answers = test.Questions;
        var userAnswers = userTest.Questions;
        
        for (var i = 0; i < answers.Count; i++)
        {
            if (userAnswers[i] == null || userAnswers[i] != answers[i])
            {
                mistake++;
                continue;
            }
            correct++;
        }
        return (correct, mistake);
    }
}