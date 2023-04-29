namespace Demo.Domain.Contracts.DomainServices
{
    public interface IBaseDomainService<TEntity> 
        where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
