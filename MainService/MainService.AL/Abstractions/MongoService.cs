using MainService.AL.Mappers;
using MainService.DAL.Abstractions;
using MongoDB.Bson;

namespace MainService.AL.Abstractions;

public abstract class MongoService<T, TDto, TKey> : IMongoService<T, TDto, TKey>
    where T : class
    where TDto : IMapper<T>
{
    protected readonly IMongoRepository<T, TKey> _repository;

    public MongoService(IMongoRepository<T, TKey> repository)
    {
        _repository = repository;
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<T> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();
        await _repository.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(TKey id, TDto dto, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"Entity with id {id} not found");

        dto.ToDto(entity);
        await _repository.UpdateAsync(id,entity, cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}