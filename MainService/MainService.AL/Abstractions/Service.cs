using MainService.DAL.Abstractions;
using MainService.AL.Features.Abstractions;
using MainService.AL.Mappers;

namespace MainService.IL.Services
{
    public abstract class Service<TEntity, TDto, TKey> : IService<TEntity, TDto, TKey>
        where TEntity : class, IEntity<TKey>
        where TDto : IMapper<TEntity>
    {
        protected readonly IRepository<TEntity, TKey> _repository;

        protected Service(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            return await _repository.GetItemByIdAsync(id, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllItemsByAsync(cancellationToken);
        }

        public virtual async Task<TEntity> CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity();
            await _repository.AddItemAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TKey id, TDto dto, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
            if (entity == null) throw new KeyNotFoundException($"Entity {id} not found");

            dto.MapTo(entity);
            await _repository.UpdateItemAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                await _repository.DeleteItemAsync(entity, cancellationToken);
            }
        }
    }
}
