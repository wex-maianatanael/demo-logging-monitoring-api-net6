using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.DomainServices;
using Demo.Domain.Entities;

namespace Demo.Application.ApplicationServices
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerDomainService _customerDomainService;
        private readonly IAccountDomainService _accountDomainService;

        public CustomerApplicationService(ICustomerDomainService customerDomainService, IAccountDomainService accountDomainService)
        {
            _customerDomainService = customerDomainService;
            _accountDomainService = accountDomainService;
        }

        public async Task<CustomerViewModel> CreateAsync(CustomerViewModel model)
        {
            var account = new Account() { 
                Number = model.Account.Number,
                CreatedDate = model.Account.CreatedDate,
                Balance = model.Account.Balance,
                Active = model.Account.Active 
                //todo: add the reference to the bank
            };

            var createdAccount = await _accountDomainService.CreateAsync(account);

            var customer = new Customer() { Name = model.Name, BirthDate = model.BirthDate, Account = createdAccount };

            var createdCustomer = await _customerDomainService.CreateAsync(customer);

            return new CustomerViewModel {  };
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CustomerViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
