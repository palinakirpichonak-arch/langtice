using MainService.AL.Abstractions;
using MainService.AL.Features.Lessons.DTO;
using MainService.DAL.Abstractions;

namespace MainService.AL.Features.Lessons.Services;

public class TestService : MongoService<Test, TestDto>, ITestService
{
    public TestService(IMongoRepository<Test> repository) : base(repository)
    {
    }
    
    public async Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken)
    {
        var test = await _repository.GetByIdAsync(testId,cancellationToken);
        ActiveTestDto activeTest = new();
        activeTest.MapTo(test);
        
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