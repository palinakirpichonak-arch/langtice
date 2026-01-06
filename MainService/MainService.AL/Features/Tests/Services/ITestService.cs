using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.DTO.Response;
using MainService.DAL.Models.TestModel;

namespace MainService.AL.Features.Tests.Services;

public interface ITestService 
{
    Task<TestResultDto> CheckTest(string testId, UserTestSubmissionDto submission, CancellationToken cancellationToken);
    public Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken);
    Task<Test?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Test>> GetAllAsync(CancellationToken cancellationToken);
    Task<Test> CreateAsync(TestDto dto, CancellationToken cancellationToken);
    Task<Test> UpdateAsync(string id, TestDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}