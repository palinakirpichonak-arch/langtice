using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Translations.Services;

public class TranslationService : ITranslationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TranslationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseTranslationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Translations.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<PaginatedList<ResponseTranslationDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Translations.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseTranslationDto>>();
        return new PaginatedList<ResponseTranslationDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseTranslationDto> CreateAsync(RequestTranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Translation>(dto);
        entity.Id = Guid.NewGuid();
        _unitOfWork.Translations.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<ResponseTranslationDto> UpdateAsync(Guid id, RequestTranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Translations.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        _mapper.Map(dto, entity);
        _unitOfWork.Translations.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Translations.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        _unitOfWork.Translations.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
