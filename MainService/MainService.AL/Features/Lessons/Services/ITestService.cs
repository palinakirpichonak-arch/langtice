using MainService.AL.Abstractions;
using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using MainService.DAL.Features.Courses.Models;
using MongoDB.Bson;

namespace MainService.AL.Features.Lessons.Services;

public interface ITestService : IMongoService<Test, TestDto, string>
{
    public Task<(int correct, int mistake)> CheckTest(string testId, UserTestDto userTest, CancellationToken cancellationToken);
    public Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken);
}