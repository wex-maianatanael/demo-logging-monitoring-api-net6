using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Demo.Domain.DomainServices
{
    public class BankDomainService : BaseDomainService<Bank>, IBankDomainService
    {
        private readonly ILogger<BankDomainService> _loggerBankDomainService;
        private readonly ILogger<BaseDomainService<Bank>> _logger;
        private readonly IBankRepository _bankRepository;

        public BankDomainService(IBankRepository bankRepository, ILogger<BaseDomainService<Bank>> logger, ILogger<BankDomainService> loggerBankDomainService)
            : base(bankRepository, logger)
        {
            _bankRepository = bankRepository;
            _loggerBankDomainService = loggerBankDomainService;
        }
    }
}
