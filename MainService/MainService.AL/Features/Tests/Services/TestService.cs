using MainService.AL.Exceptions;
using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Data.Lessons;
using MainService.DAL.Data.Tests;
using MainService.DAL.Data.UserTest;
using MainService.DAL.Features.Test;
using MapsterMapper;
using MongoDB.Bson;

namespace MainService.AL.Features.Tests.Services;

public class TestService : ITestService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUserTestRepository _userTestRepository;
    private readonly ITestRepository _testRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TestService(
        ILessonRepository lessonRepository,
        IUserTestRepository userTestRepository,
        ITestRepository testRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _userTestRepository = userTestRepository;
        _testRepository = testRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(testId, cancellationToken);
        if (test is null)
            throw new NotFoundException($"Test not found");
        return _mapper.Map<ActiveTestDto>(test);
    }

    public async Task<Test?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var test =  await _testRepository.GetByIdAsync(id, cancellationToken);
        
        if (test is null)
            throw new NotFoundException($"Test not found");
        
        return test;
    }

    public async Task<IEnumerable<Test>> GetAllByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        var lesson = (await _lessonRepository.GetAsync(
            filter: l => l.Id == lessonId,
            tracking: false,
            cancellationToken: cancellationToken)).FirstOrDefault();
        var allTests = await _testRepository.GetAllAsync(cancellationToken);
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
        
        if (test is null) 
            throw new NotFoundException($"Test {id} not found");

        _mapper.Map(dto, test);
        await _testRepository.UpdateAsync(id, test, cancellationToken);
        return test;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _testRepository.DeleteAsync(id, cancellationToken);
        
        var lessons = await _lessonRepository.GetAsync(
            filter: l => l.TestId == id,
            tracking: true,
            cancellationToken: cancellationToken);

        foreach (var lesson in lessons)
        {
            lesson.TestId = null;
            _lessonRepository.UpdateItem(lesson);
        }
        
        var userTests = await _userTestRepository.GetAsync(
            filter: ut => ut.TestId == id,
            tracking: true,
            cancellationToken: cancellationToken);

        foreach (var ut in userTests)
        {
            _userTestRepository.DeleteItem(ut);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task<(int correct, int mistake)> CheckTest(string testId, UserAnswerDto userTest, CancellationToken cancellationToken)
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
