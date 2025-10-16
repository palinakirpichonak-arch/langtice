using MainService.AL.Features.Tests.DTO.Request;
using MainService.AL.Features.Tests.Services;
using MainService.AL.Features.UserTests.DTO.Request;
using MainService.AL.Features.UserTests.DTO.Response;
using MainService.BLL.Data.Tests;
using MainService.BLL.Services.LLM;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Test;
using MainService.DAL.Features.UserTest;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.UserTests.Services;

public class UserTestService : IUserTestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestRepository  _testRepository;
    private readonly IMapper _mapper;
    private readonly ILlmClient _llmClient;
    private readonly ITestService _testService;

    public UserTestService(IUnitOfWork unitOfWork, IMapper mapper, ILlmClient llmClient, ITestService testService, ITestRepository testRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _llmClient = llmClient;
        _testService = testService;
        _testRepository = testRepository;
    }

    public async Task<IEnumerable<UserTest>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userTests = await _unitOfWork.UserTests.GetAllItemsAsync(cancellationToken);
        return userTests.Where(ut => ut.UserId == userId);
    }

    public async Task<ResponseUserTestDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.UserTests.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseUserTestDto>(entity);
    }

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserTests.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseUserTestDto>>();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllWithUserIdAsync(
        Guid userId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var entities =
            await _unitOfWork.UserTests.GetAllItemsWithIdAsync(userId, pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseUserTestDto>>();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseUserTestDto> CreateAsync(RequestUserTestDto dto, CancellationToken cancellationToken)
    {
        var userWordsPaginated =
            await _unitOfWork.UserWords.GetAllByUserIdAsync(dto.UserId, 1, dto.Count, cancellationToken);
        var userWords = userWordsPaginated.Items.ToList();

        if (!userWords.Any())
            throw new InvalidOperationException("No user words available to generate test.");

        var languageIds = userWords.Select(uw => uw.Word.LanguageId).Distinct().ToList();
        var allLanguages = await _unitOfWork.Languages.GetAllItemsAsync(cancellationToken);
        var languageMap = allLanguages
            .Where(l => languageIds.Contains(l.Id))
            .ToDictionary(l => l.Id, l => l.Name);

        var wordLanguageMap = userWords.ToDictionary(
            uw => uw.Word.Text,
            uw => languageMap.TryGetValue(uw.Word.LanguageId, out var name) ? name : "English"
        );

        var wordsList = string.Join(", ", wordLanguageMap.Select(kv => $"'{kv.Key}' ({kv.Value})"));
        var prompt =
            "Generate vocabulary test questions for the following words. " +
            "For each word, generate 2 fill-in-the-blank sentences in the specified language, " +
            "each with 3 answer options (one correct). " +
            "Return a JSON object where each key is the word and each value is an array of questions.\n" +
            $"Example: {{ \"Hello\": [{{\"Sentence\":..., \"AnswerOptions\": [...], \"CorrectAnswer\": ...}}, ...] }}\n" +
            $"Words: {wordsList}";

        var llmResponse = await _llmClient.SendRequestAsync(prompt);

        Dictionary<string, List<Question>>? generatedTests = null;
        try
        {
            generatedTests = System.Text.Json.JsonSerializer
                .Deserialize<Dictionary<string, List<Question>>>(llmResponse);
        }
        catch
        {
            generatedTests = new Dictionary<string, List<Question>>();
        }

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
            UserId = dto.UserId,
            Name = dto.Name,
            Description = dto.Description,
            OrderNum = dto.OrderNum,
            TestId = testEntity.Id
        };
        
        try
        {
            _unitOfWork.UserTests.AddItem(userTest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _testService.DeleteAsync(testEntity.Id, CancellationToken.None);
            throw;
        }
        
        return _mapper.Map<ResponseUserTestDto>(userTest);
    }


    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userTest = await _unitOfWork.UserTests.GetItemByIdAsync(id, cancellationToken);
        if (userTest is null)
            throw new KeyNotFoundException($"UserTest {id} not found");

        if (!string.IsNullOrEmpty(userTest.TestId))
        {
            await _testRepository.DeleteAsync(userTest.TestId, cancellationToken);
        }

        _unitOfWork.UserTests.DeleteItem(userTest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}