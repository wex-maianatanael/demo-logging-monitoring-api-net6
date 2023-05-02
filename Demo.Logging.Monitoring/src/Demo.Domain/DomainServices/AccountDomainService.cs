using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Demo.Domain.DomainServices
{
    public class AccountDomainService : BaseDomainService<Account>, IAccountDomainService
    {
        private readonly ILogger<AccountDomainService> _loggerAccountDomainService;
        private readonly ILogger<BaseDomainService<Account>> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountDomainService(IAccountRepository accountRepository, ILogger<AccountDomainService> loggerAccountDomainService, ILogger<BaseDomainService<Account>> logger)
            : base(accountRepository, logger)
        {
            _accountRepository = accountRepository;
            _loggerAccountDomainService = loggerAccountDomainService;
        }
    }
}
