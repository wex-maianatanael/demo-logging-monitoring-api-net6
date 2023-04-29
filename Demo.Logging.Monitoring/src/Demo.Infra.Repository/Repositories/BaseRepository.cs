using Demo.Domain.Contracts.Repositories;
using Demo.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infra.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly BankDbContext _dbContext;

        protected BaseRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            
            return _dbContext.Entry(entity).Entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
