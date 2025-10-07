using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using MainService.BLL.Data.Lessons;
using MainService.DAL.Features.Courses.Models;
using MapsterMapper;
using MongoDB.Bson;

namespace MainService.AL.Features.Lessons.Services;

public class TestService : ITestService
{
   private readonly ITestRepository _testRepository;
   private readonly ILessonRepository _lessonRepository;
   private readonly IMapper _mapper;

   public TestService(
       ITestRepository repository, 
       ILessonRepository lessonRepository, 
       IMapper mapper)
   {
       _testRepository = repository;
       _lessonRepository = lessonRepository;
       _mapper = mapper;
   }
    
    public async Task<ActiveTestDto> GetActiveTest(string testId, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(testId,cancellationToken);
        var activeTest =_mapper.Map<ActiveTestDto>(test);
        
        return activeTest;
    }

    public async Task<Test?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var test = await _testRepository.GetByIdAsync(id, cancellationToken);
        return test;
    }

    public async Task<IEnumerable<Test>> GetAllByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        var userCourse =  await _testRepository.GetAllAsync(cancellationToken);
        var lesson = await _lessonRepository.GetItemByIdAsync(lessonId, cancellationToken);
        return userCourse.Select(t => t).Where(t => t.Id == lesson.TestId);
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
        var lessons = await _lessonRepository.GetAllItemsAsync(cancellationToken);
        var lessonsToUpdate =  lessons.Where(l => l.TestId == id);
        
        foreach (var lesson in lessonsToUpdate)
        {
            lesson.TestId = null;
           await _lessonRepository.UpdateItemAsync(lesson, cancellationToken);
        }
    }

    public async Task<(int correct, int mistake)> CheckTest(string testId, UserTestDto userTest, CancellationToken cancellationToken)
    {
        (int correct, int mistake) = (0, 0);
        var test = await _testRepository.GetByIdAsync(testId,cancellationToken);
        
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