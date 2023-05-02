using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;
using Microsoft.Extensions.Logging;

namespace Demo.Infra.Repository.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        // todo: we must need to opt for one or another between ILogger and factory logger

        private readonly ILogger<BaseRepository<Bank>> _logger;
        private readonly ILogger _factoryLogger;
        private readonly ILogger<BankRepository> _loggerBankRepository;
        private readonly BankDbContext _dbContext;

        public BankRepository(BankDbContext dbContext, ILogger<BaseRepository<Bank>> logger, ILogger<BankRepository> loggerBankRepository, ILoggerFactory loggerFactory)
            : base(dbContext, logger, loggerFactory)
        {
            _dbContext = dbContext;
            _loggerBankRepository = loggerBankRepository;
            // todo: check if we do need to instantiate the logger factory here as we did in the base repository class
        }
    }
}
