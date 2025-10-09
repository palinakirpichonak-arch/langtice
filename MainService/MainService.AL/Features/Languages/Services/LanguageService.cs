using MainService.AL.Features.Languages.DTO.Request;
using MainService.AL.Features.Languages.DTO.Response;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Languages.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Languages.Services;

public class LanguageService : ILanguageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseLanguageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Languages.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<IEnumerable<ResponseLanguageDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Languages.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseLanguageDto>>(entities);
    }

    public async Task<PaginatedList<ResponseLanguageDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Languages.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseLanguageDto>>();
        return new PaginatedList<ResponseLanguageDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseLanguageDto> CreateAsync(RequestLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Language>(dto);
        entity.Id = Guid.NewGuid();

        _unitOfWork.Languages.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<ResponseLanguageDto> UpdateAsync(Guid id, ResponseLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Languages.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Language with id {id} not found");

        _mapper.Map(dto, entity);
        _unitOfWork.Languages.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Languages.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Language with id {id} not found");

        _unitOfWork.Languages.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
