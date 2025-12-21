using System.Text.Json;
using MainService.AL.Exceptions;
using MainService.AL.Features.Llm;
using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.Services;
using MainService.AL.Features.UserTests.DTO.Request;
using MainService.AL.Features.UserTests.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Models.TestModel;
using MainService.DAL.Models.UserTestModel;
using MainService.DAL.Repositories.Languages;
using MainService.DAL.Repositories.Tests;
using MainService.DAL.Repositories.UserTests;
using MainService.DAL.Repositories.UserWords;
using MapsterMapper;

namespace MainService.AL.Features.UserTests.Services;

public class UserTestService : IUserTestService
{
    private readonly IUserTestRepository _userTestRepository;
    private readonly IUserWordRepository _userWordRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestRepository _testRepository;
    private readonly IMapper _mapper;
    private readonly ILlmService _llmService;
    private readonly ITestService _testService;

    public UserTestService(
        IUserTestRepository userTestRepository,
        IUserWordRepository userWordRepository,
        ILanguageRepository languageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILlmService llmService,
        ITestService testService,
        ITestRepository testRepository)
    {
        _userTestRepository = userTestRepository;
        _userWordRepository = userWordRepository;
        _languageRepository = languageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _llmService = llmService;
        _testService = testService;
        _testRepository = testRepository;
    }

    public async Task<IEnumerable<UserTest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userTests = await _userTestRepository.GetAsync(
            filter: ut => ut.UserId == userId,
            tracking: false,
            cancellationToken: cancellationToken);

        return userTests;
    }

    public async Task<ResponseUserTestDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = (await _userTestRepository.GetAsync(
            filter: ut => ut.Id == id,
            tracking: false,
            cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (entity is null)
            throw new NotFoundException("User test not found");

        return _mapper.Map<ResponseUserTestDto>(entity);
    }

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _userTestRepository.GetAsync(pageIndex: pageIndex,
            pageSize: pageSize,
            tracking: false, cancellationToken: cancellationToken);

        var list = entities.Select(_mapper.Map<ResponseUserTestDto>).ToList();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllWithUserIdAsync(
        Guid userId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _userTestRepository.GetAsync(
            filter: ut => ut.UserId == userId,
            pageIndex: pageIndex,
            pageSize: pageSize,
            tracking: false, cancellationToken: cancellationToken);

        var list = entities.Select(_mapper.Map<ResponseUserTestDto>).ToList();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }
    public async Task<ResponseUserTestDto> CreateAsync(RequestUserTestDto dto, Guid userId, CancellationToken cancellationToken)
    {
        var userWords = (await _userWordRepository.GetAsync(
            filter: uw => uw.UserId == userId,
            pageIndex: 1,
            pageSize: dto.Count,
            tracking: false, 
            cancellationToken: cancellationToken,
            includes: uw => uw.Word))
            .ToList();

        if (!userWords.Any())
            throw new NotFoundException("No user words available to generate test.");

        var languageIds = userWords.Select(uw => uw.Word.LanguageId).Distinct().ToList();
        var allLanguages = await _languageRepository.GetAsync(
            tracking: false,
            cancellationToken: cancellationToken);

        var languageMap = allLanguages
            .Where(l => languageIds.Contains(l.Id))
            .ToDictionary(l => l.Id, l => l.Name);

        var wordLanguageMap = userWords.ToDictionary(
            uw => uw.Word.Text,
            uw => languageMap.TryGetValue(uw.Word.LanguageId, out var name) ? name : "English"
        );

        var wordsList = string.Join(", ", wordLanguageMap.Select(kv => $"'{kv.Key}' ({kv.Value})"));
        
        System.Console.WriteLine(wordsList);
        
        var prompt =
            "Generate vocabulary test questions for the following words. " +
            "For each word, generate 2 fill-in-the-blank sentences in the specified language, " +
            "each with 3 answer options (one correct). Don't use words with close context in one sentence (for example, if the cat and dog or other words can be both correct in one sentence)" +
            "Return a plain formatted JSON object only (no sentences in the beggining) where each key is the word and each value is an array of questions.\n" +
            $"Example: {{ \"Hello\": [{{\"Sentence\":..., \"AnswerOptions\": [...], \"CorrectAnswer\": ...}}, ...] }}\n" +
            $"Words: {wordsList}";

        var llmResponse = await _llmService.ProcessPromptAsync(prompt, cancellationToken);

        Dictionary<string, List<Question>>? generatedTests;
        try
        {
            generatedTests = JsonSerializer.Deserialize<Dictionary<string, List<Question>>>(llmResponse);
        }
        catch
        {
            generatedTests = new Dictionary<string, List<Question>>();
        }

         System.Console.WriteLine(generatedTests);

        var questions = new List<Question>();
        int questionNumber = 1;

        foreach (var userWord in userWords)
        {
            if (generatedTests!.TryGetValue(userWord.Word.Text, out var wordQuestions) && wordQuestions.Any())
            {
                foreach (var q in wordQuestions)
                {
                    q.QuestionNumber = questionNumber++;
                    questions.Add(q);
                }
            }
            else
            {
                questions.Add(new Question
                {
                    QuestionNumber = questionNumber++,
                    Sentence = $"Fill in the blank: {userWord.Word.Text}",
                    AnswerOptions = new[] { userWord.Word.Text },
                    CorrectAnswer = userWord.Word.Text
                });
            }
        }

        var test = new TestDto
        {
            Title = dto.Name,
            Questions = questions
        };

        var testEntity = await _testService.CreateAsync(test, cancellationToken);

        var userTest = new UserTest
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = dto.Name,
            Description = dto.Description,
            OrderNum = dto.OrderNum,
            TestId = testEntity.Id
        };

        try
        {
            _userTestRepository.AddItem(userTest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await _testService.DeleteAsync(testEntity.Id, CancellationToken.None);
            throw;
        }

        return _mapper.Map<ResponseUserTestDto>(userTest);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userTest = (await _userTestRepository.GetAsync(
            filter: ut => ut.Id == id,
            tracking: false,
            cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (userTest is null)
            throw new NotFoundException($"UserTest {id} not found");

        if (!string.IsNullOrEmpty(userTest.TestId))
        {
            await _testRepository.DeleteAsync(userTest.TestId, cancellationToken);
        }

        _userTestRepository.DeleteItem(userTest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
