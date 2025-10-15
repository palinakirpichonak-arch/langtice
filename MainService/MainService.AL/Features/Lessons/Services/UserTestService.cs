using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.DTO.Response;
using MainService.BLL.Services;
using MainService.BLL.Services.LLM;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;
using MainService.DAL.Features.Users.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Lessons.Services;

public class UserTestService : IUserTestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILlmClient  _llmClient;
    private readonly ITestService _testService;

    public UserTestService(IUnitOfWork unitOfWork, IMapper mapper,  ILlmClient llmClient,  ITestService testService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _llmClient = llmClient;
        _testService = testService;
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

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserTests.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseUserTestDto>>();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }

    public async Task<PaginatedList<ResponseUserTestDto>> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserTests.GetAllItemsWithIdAsync(userId, pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseUserTestDto>>();
        return new PaginatedList<ResponseUserTestDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseUserTestDto> CreateAsync(RequestUserTestDto dto, CancellationToken cancellationToken)
    {
        var userWordsPaginated = await _unitOfWork.UserWords.GetAllByUserIdAsync(dto.UserId, 1, dto.Count, cancellationToken);
        var userWords = userWordsPaginated.Items.ToList();

        if (!userWords.Any())
            throw new InvalidOperationException("No user words available to generate test.");
        
        var questions = new List<Question>();
        int questionNumber = 1;
        
        var languageIds = userWords.Select(uw => uw.Word.LanguageId).Distinct().ToList();
        var allLanguages = await _unitOfWork.Languages.GetAllItemsAsync(cancellationToken);
        var languageMap = allLanguages
            .Where(l => languageIds.Contains(l.Id))
            .ToDictionary(l => l.Id, l => l.Name);
        
        foreach (var userWord in userWords)
        {
            var langName = languageMap.TryGetValue(userWord.Word.LanguageId, out var name)
                ? name
                : "English";
            
            var prompt = $"Generate 2 sentences in '{langName}' where the word '{userWord.Word.Text}' is missing. " +
                         "Provide 3 options (one correct) for each sentence. " +
                         "Return as JSON array: [{\"Sentence\":..., \"AnswerOptions\": [...], \"CorrectAnswer\": ...}, ...]";
            
            var llmResponse = await _llmClient.SendRequestAsync(prompt);
            
            try
            {
                var generatedQuestions = System.Text.Json.JsonSerializer
                    .Deserialize<List<Question>>(llmResponse);

                if (generatedQuestions != null)
                {
                    foreach (var q in generatedQuestions)
                    {
                        q.QuestionNumber = questionNumber++;
                        questions.Add(q);
                    }
                }
            }
            catch(Exception e)
            {
                questions.Add(new Question
                {
                    QuestionNumber = questionNumber++,
                    Sentence = $"Fill in the blank: {userWord.Word.Text} (LLM failed to generate sentence)",
                    AnswerOptions = new[] { userWord.Word.Text },
                    CorrectAnswer = userWord.Word.Text
                });
            }
        }
        
        TestDto test = new()
        {
            Title = dto.Name,
            Questions = questions
        };
        var testEntity = await _testService.CreateAsync(test,cancellationToken);
        
        var userTest = new UserTest
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            Name = dto.Name,
            Description = dto.Description,
            OrderNum = dto.OrderNum,
            TestId = testEntity.Id,
        };

        _unitOfWork.UserTests.AddItem(userTest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ResponseUserTestDto>(userTest);
    }


    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userTest = await _unitOfWork.UserTests.GetItemByIdAsync(id, cancellationToken);
        if (userTest is null)
            throw new KeyNotFoundException($"UserTest {id} not found");
        
        if (!string.IsNullOrEmpty(userTest.TestId))
        {
            await _testService.DeleteAsync(userTest.TestId, cancellationToken);
        }
        
        _unitOfWork.UserTests.DeleteItem(userTest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
