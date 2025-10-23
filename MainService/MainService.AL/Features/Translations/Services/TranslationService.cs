using MainService.AL.Exceptions;
using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Data.Translations;
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
        var entity = (await _translationRepository.GetAsync(
            tracking: false,
            filter: t => t.Id == id,
            cancellationToken: cancellationToken,
            includes:
            [
                t => t.FromWord,
                t => t.ToWord
            ])).FirstOrDefault();
        
        if (entity == null)
            throw new NotFoundException("Translation not found");
        
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<IEnumerable<ResponseTranslationDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _translationRepository.GetAsync(
                tracking: false,
                pageIndex: pageIndex,
                pageSize: pageSize,
                cancellationToken: cancellationToken,
                includes:
                [
                    t => t.FromWord,
                    t => t.ToWord
                ]);
        
        var list = entities.Select(t => new ResponseTranslationDto
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

        return list;
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
        var entity = (await _translationRepository.GetAsync(
            filter: c => c.Id == id,
            tracking: true,
            cancellationToken: cancellationToken,
            includes:
            [
                t => t.FromWord,
                t => t.ToWord
            ])).FirstOrDefault();
        
        if (entity is null) 
            throw new NotFoundException($"Translation {id} not found");

        _mapper.Map(dto, entity);
        _translationRepository.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = (await _translationRepository.GetAsync(
            filter: t => t.Id == id,
            tracking: false,
            cancellationToken: cancellationToken)).FirstOrDefault();
        
        if (entity is null) 
            throw new NotFoundException($"Translation {id} not found");

        _translationRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
