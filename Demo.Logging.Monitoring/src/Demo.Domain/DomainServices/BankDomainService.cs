using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;

namespace Demo.Domain.DomainServices
{
    public class BankDomainService : BaseDomainService<Bank>, IBankDomainService
    {
        private readonly IBankRepository _bankRepository;

        public BankDomainService(IBankRepository bankRepository)
            : base(bankRepository)
        {
            _bankRepository = bankRepository;
        }
    }
}
