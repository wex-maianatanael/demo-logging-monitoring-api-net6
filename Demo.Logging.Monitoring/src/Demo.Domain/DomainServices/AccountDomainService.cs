using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;

namespace Demo.Domain.DomainServices
{
    public class AccountDomainService : BaseDomainService<Account>, IAccountDomainService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountDomainService(IAccountRepository accountRepository)
            : base(accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}
