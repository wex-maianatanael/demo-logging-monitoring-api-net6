using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Demo.Domain.DomainServices
{
    public abstract class BaseDomainService<TEntity> : IBaseDomainService<TEntity>
        where TEntity : class
    {
        private readonly ILogger<BaseDomainService<TEntity>> _logger;
        private readonly IBaseRepository<TEntity> _repository;

        protected BaseDomainService(IBaseRepository<TEntity> repository, ILogger<BaseDomainService<TEntity>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            _logger.LogDebug("Getting all {TEntity}s from database.", typeof(TEntity).Name);

            Activity.Current?.AddEvent(new ActivityEvent("Event - Getting data from database"));
            
            var result = await _repository.GetAllAsync();
            
            Activity.Current?.AddEvent(new ActivityEvent("Event - Retrieved data from database"));

            return result;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting {TEntity} in domain service based on its ID {id}", typeof(TEntity).Name, id);
            
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }
    }
}
