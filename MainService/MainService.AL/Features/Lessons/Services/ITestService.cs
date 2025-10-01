using MainService.AL.Features.Lessons.DTO;

namespace MainService.AL.Features.Lessons.Services;

public interface ITestService : IMongoService<Test, TestDto>
{
    public Task<(int correct, int mistake)> CheckTest(string testId, UserTestDto userTest, CancellationToken cancellationToken);
    public Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken);
}