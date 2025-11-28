using MainService.AL.Exceptions;
using MainService.AL.Features.Languages.DTO.Request;
using MainService.AL.Features.Languages.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Models.LanguagesModel;
using MainService.DAL.Repositories.Language_;
using MapsterMapper;

namespace MainService.AL.Features.Languages.Services;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _languageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LanguageService(
        ILanguageRepository languageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _languageRepository = languageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseLanguageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = (await _languageRepository.GetAsync(
            filter: l => l.Id == id,
            tracking: false,
            cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (entity is null)
            throw new NotFoundException("Language not found");

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<IEnumerable<ResponseLanguageDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _languageRepository.GetAsync(pageIndex: pageIndex,
            pageSize: pageSize,
            tracking: false, cancellationToken: cancellationToken);

        return _mapper.Map<IEnumerable<ResponseLanguageDto>>(entities);
    }

    public async Task<ResponseLanguageDto> CreateAsync(RequestLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Language>(dto);
        entity.Id = Guid.NewGuid();

        _languageRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<ResponseLanguageDto> UpdateAsync(Guid id, ResponseLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = (await _languageRepository.GetAsync(
                filter: l => l.Id == id,
                tracking: true,
                cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (entity is null)
            throw new NotFoundException("Language not found");

        _mapper.Map(dto, entity);
        _languageRepository.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = (await _languageRepository.GetAsync(
            filter: l => l.Id == id,
            tracking: false,
            cancellationToken: cancellationToken))
            .FirstOrDefault();

        if (entity is null)
            throw new NotFoundException("Language not found");

        _languageRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
