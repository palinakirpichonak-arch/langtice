using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Data.Translations;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations;
using MapsterMapper;

namespace MainService.AL.Features.Translations.Services;

public class TranslationService : ITranslationService
{
    private readonly ITranslationRepository  _translationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TranslationService(
        ITranslationRepository translationRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _translationRepository = translationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseTranslationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _translationRepository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<PaginatedList<ResponseTranslationDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _translationRepository
            .GetAllItemsAsync(cancellationToken);

        var pagedEntities = entities
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var list = pagedEntities.Select(t => new ResponseTranslationDto
        {
            Id = t.Id,
            CourseId = t.CourseId,
            FromWord = new ResponseWordDto
            {
                Id = t.FromWord.Id,
                Text = t.FromWord.Text
            },
            ToWord = new ResponseWordDto
            {
                Id = t.ToWord.Id,
                Text = t.ToWord.Text
            }
        }).ToList();

        return new PaginatedList<ResponseTranslationDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseTranslationDto> CreateAsync(RequestTranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Translation>(dto);
        entity.Id = Guid.NewGuid();
        _translationRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<ResponseTranslationDto> UpdateAsync(Guid id, RequestTranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = await _translationRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        _mapper.Map(dto, entity);
        _translationRepository.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _translationRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        _translationRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
