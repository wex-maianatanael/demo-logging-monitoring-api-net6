using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Demo.Domain.Contracts.DomainServices;

namespace Demo.Application.ApplicationServices
{
    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly IAccountDomainService _domainService;

        public AccountApplicationService(IAccountDomainService domainService)
        {
            _domainService = domainService;
        }

        public Task<AccountViewModel> CreateAsync(AccountViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AccountViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
