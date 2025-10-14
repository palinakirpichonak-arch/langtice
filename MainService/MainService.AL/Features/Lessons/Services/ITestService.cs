using MainService.AL.Features.Lessons.DTO.Request;
using MainService.DAL.Features.Lessons;

namespace MainService.AL.Features.Lessons.Services;

public interface ITestService 
{
    public Task<(int correct, int mistake)> CheckTest(string testId, UserAnswerDto userTest, CancellationToken cancellationToken);
    public Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken);
    Task<Test?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Test>> GetAllByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken);
    Task<IEnumerable<Test>> GetAllAsync(CancellationToken cancellationToken);
    Task<Test> CreateAsync(TestDto dto, CancellationToken cancellationToken);
    Task<Test> UpdateAsync(string id, TestDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}