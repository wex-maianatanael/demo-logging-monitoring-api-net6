using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;
using Microsoft.Extensions.Logging;

namespace Demo.Infra.Repository.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        // todo: we must need to opt for one or another between ILogger and factory logger

        private readonly ILogger<BaseRepository<Account>> _logger;
        private readonly ILogger _factoryLogger;
        private readonly ILogger<AccountRepository> _loggerAccountRepository;
        private readonly BankDbContext _dbContext;

        public AccountRepository(BankDbContext dbContext, ILogger<BaseRepository<Account>> logger, ILogger<AccountRepository> loggerAccountRepository, ILoggerFactory loggerFactory)
            : base(dbContext, logger, loggerFactory)
        {
            _dbContext = dbContext;
            _loggerAccountRepository = loggerAccountRepository;
            // todo: check if we do need to instantiate the logger factory here as we did in the base repository class
        }
    }
}
