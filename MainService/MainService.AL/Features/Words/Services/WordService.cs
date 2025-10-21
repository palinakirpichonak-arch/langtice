using MainService.AL.Features.LLM;
using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Data.Courses;
using MainService.DAL.Data.Translations;
using MainService.DAL.Data.Words;
using MainService.DAL.Features.Translations;
using MainService.DAL.Features.Words;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Words.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ITranslationRepository  _translationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILlmService _llmService;

    public WordService(
        IWordRepository wordRepository,
        ICourseRepository courseRepository,
        ITranslationRepository translationRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper,  
        ILlmService llmService)
    {
        _wordRepository = wordRepository;
        _courseRepository = courseRepository;
        _translationRepository = translationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _llmService = llmService;
    }

    public async Task<ResponseWordDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _wordRepository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<PaginatedList<ResponseWordDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _wordRepository.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseWordDto>>();
        return new PaginatedList<ResponseWordDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseWordDto> CreateAsync(RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Word>(dto);
        entity.Id = Guid.NewGuid();

        _wordRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<ResponseWordDto> UpdateAsync(Guid id, RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = await _wordRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        _mapper.Map(dto, entity);
        _wordRepository.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _wordRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        _wordRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<ResponseTranslateWordDto> TranslateWordAsync(RequestTranslateWordDto dto, CancellationToken cancellationToken)
{
    var course = await _courseRepository.GetItemByIdAsync(dto.CourseId, cancellationToken);
    if (course is null)
        throw new KeyNotFoundException($"Course {dto.CourseId} not found");

    Word? sourceWord = null;

    if (dto.WordId.HasValue)
    {
        sourceWord = await _wordRepository.GetItemByIdAsync(dto.WordId.Value, cancellationToken);
    }
    else if (!string.IsNullOrWhiteSpace(dto.Text))
    {
        var allWords = await _wordRepository.GetAllItemsAsync(cancellationToken);
        sourceWord = allWords.FirstOrDefault(w => w.Text.ToLower() == dto.Text.ToLower());
    }

    if (sourceWord is null)
    {
        sourceWord = new Word
        {
            Id = Guid.NewGuid(),
            Text = dto.Text ?? throw new ArgumentException("Text must be provided"),
            LanguageId = course.BaseLanguageId
        };
        _wordRepository.AddItem(sourceWord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    var translations = await _translationRepository.GetAllItemsAsync(cancellationToken);
    var existingTranslation = translations.FirstOrDefault(t =>
        t.CourseId == course.Id && t.FromWordId == sourceWord.Id);

    if (existingTranslation != null)
    {
        var targetWord = await _wordRepository.GetItemByIdAsync(existingTranslation.ToWordId, cancellationToken);
        return new ResponseTranslateWordDto
        {
            FromWordId = sourceWord.Id,
            FromText = sourceWord.Text,
            ToWordId = targetWord!.Id,
            ToText = targetWord.Text,
            CourseId = course.Id
        };
    }

    var baseLang = course.BaseLanguage.Name;
    var learnLang = course.LearningLanguage.Name;
    
    var prompt = $"Translate the word \"{sourceWord.Text}\" from {baseLang} to {learnLang}. Respond with only the translated word.";
    var translatedText = await _llmService.ProcessPromptAsync(prompt, cancellationToken);

    var targetWordEntity = new Word
    {
        Id = Guid.NewGuid(),
        Text = translatedText.Trim(),
        LanguageId = course.LearningLanguageId
    };
    _wordRepository.AddItem(targetWordEntity);

    var translation = new Translation
    {
        Id = Guid.NewGuid(),
        CourseId = course.Id,
        FromWordId = sourceWord.Id,
        ToWordId = targetWordEntity.Id
    };

    _translationRepository.AddItem(translation);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ResponseTranslateWordDto
    {
        FromWordId = sourceWord.Id,
        FromText = sourceWord.Text,
        ToWordId = targetWordEntity.Id,
        ToText = targetWordEntity.Text,
        CourseId = course.Id
    };
}
}
