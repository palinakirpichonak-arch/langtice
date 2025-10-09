using MainService.AL.Features.Lessons.DTO.Request;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MapsterMapper;
using MongoDB.Bson;

namespace MainService.AL.Features.Lessons.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TestService(
        ITestRepository testRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _testRepository = testRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(testId, cancellationToken);
        return _mapper.Map<ActiveTestDto>(test);
    }

    public async Task<Test?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _testRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Test>> GetAllByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        var allTests = await _testRepository.GetAllAsync(cancellationToken);
        var lesson = await _unitOfWork.Lessons.GetItemByIdAsync(lessonId, cancellationToken);
        return allTests.Where(t => t.Id == lesson.TestId);
    }

    public async Task<IEnumerable<Test>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _testRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Test> CreateAsync(TestDto dto, CancellationToken cancellationToken)
    {
        var test = _mapper.Map<Test>(dto);
        test.Id = ObjectId.GenerateNewId().ToString();
        await _testRepository.AddAsync(test, cancellationToken);
        return test;
    }

    public async Task<Test> UpdateAsync(string id, TestDto dto, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(id, cancellationToken);
        if (test is null) throw new KeyNotFoundException($"Test {id} not found");

        _mapper.Map(dto, test);
        await _testRepository.UpdateAsync(id, test, cancellationToken);
        return test;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _testRepository.DeleteAsync(id, cancellationToken);

        var lessons = await _unitOfWork.Lessons.GetAllItemsAsync(cancellationToken);
        var lessonsToUpdate = lessons.Where(l => l.TestId == id);

        foreach (var lesson in lessonsToUpdate)
        {
            lesson.TestId = null;
            _unitOfWork.Lessons.UpdateItem(lesson);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(int correct, int mistake)> CheckTest(string testId, UserTestDto userTest, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(testId, cancellationToken);

        (int correct, int mistake) = (0, 0);
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
