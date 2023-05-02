using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;
using Microsoft.Extensions.Logging;

namespace Demo.Infra.Repository.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        // todo: we must need to opt for one or another between ILogger and factory logger

        private readonly ILogger<BaseRepository<Customer>> _logger;
        private readonly ILogger _factoryLogger;
        private readonly ILogger<CustomerRepository> _loggerCustomerRepository;
        private readonly BankDbContext _dbContext;

        public CustomerRepository(BankDbContext dbContext, ILogger<BaseRepository<Customer>> logger, ILogger<CustomerRepository> loggerCustomerRepository, ILoggerFactory loggerFactory)
            : base(dbContext, logger, loggerFactory)
        {
            _dbContext = dbContext;
            _loggerCustomerRepository = loggerCustomerRepository;
            // todo: check if we do need to instantiate the logger factory here as we did in the base repository class
        }
    }
}
