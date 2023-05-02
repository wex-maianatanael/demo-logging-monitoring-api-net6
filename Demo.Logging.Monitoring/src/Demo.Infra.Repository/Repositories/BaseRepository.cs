using Demo.Domain.Contracts.Repositories;
using Demo.Infra.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Demo.Infra.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        // todo: We must need to opt for one or another between ILogger and factory logger. They were used together here just for test purpose.

        private readonly ILogger<BaseRepository<TEntity>> _logger;
        private readonly ILogger _factoryLogger;
        private readonly BankDbContext _dbContext;

        protected BaseRepository(BankDbContext dbContext, ILogger<BaseRepository<TEntity>> logger, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = logger;
            _factoryLogger = loggerFactory.CreateLogger("BaseRepositoryLayer");
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
            _logger.LogInformation("Querying {TEntity}s in the repository layer.", typeof(TEntity).Name);

            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                throw new Exception("forced exception.");

                var timer = new Stopwatch();
                timer.Start();

                var result = await _dbContext.Set<TEntity>().FindAsync(id);

                timer.Stop();

                _logger.LogDebug("Querying {TEntity}s for {id} finished in {milliseconds} milliseconds.", typeof(TEntity).Name, id, timer.ElapsedMilliseconds);

                _factoryLogger.LogInformation("(FactoryLogger) Querying {TEntity}s for {id} finished in {ticks} ticks.", typeof(TEntity).Name, id, timer.ElapsedTicks);

                return result;
            }
            catch (Exception ex)
            {
                var newEx = new DbUpdateException("Something bad happened in database.", ex);
                newEx.Data.Add(typeof(TEntity).Name, id);
                throw newEx;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
