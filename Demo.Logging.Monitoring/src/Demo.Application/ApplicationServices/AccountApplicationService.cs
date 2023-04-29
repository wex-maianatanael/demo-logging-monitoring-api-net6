using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.DomainServices;
using Demo.Domain.Entities;

namespace Demo.Application.ApplicationServices
{
    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly IAccountDomainService _accountDomainService;

        public AccountApplicationService(IAccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
        }

        public async Task<AccountViewModel> CreateAsync(AccountViewModel model)
        {
            var account = new Account()
            {
                ID = model.ID,
                Active = model.Active,
                Balance = model.Balance,
                CreatedDate = model.CreatedDate,
                Number = model.Number,
                BankID = model.BankID,
                CustomerID = model.CustomerID
            };

            var createdAccount = await _accountDomainService.CreateAsync(account);

            // todo: use automapper here
            return new AccountViewModel() 
            {
                ID = createdAccount.ID,
                Active = createdAccount.Active,
                Balance = createdAccount.Balance,
                CreatedDate = createdAccount.CreatedDate,
                Number = createdAccount.Number,
                BankID = createdAccount.BankID,
                CustomerID = createdAccount.CustomerID
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var account = await _accountDomainService.GetByIdAsync(id);

            return await _accountDomainService.DeleteAsync(account);
        }

        public async Task<List<AccountViewModel>> GetAllAsync()
        {
            var accounts = await _accountDomainService.GetAllAsync();

            // todo: use automapper here
            var accountsViewModel = new List<AccountViewModel>();
            foreach (var account in accounts)
            {
                var accountViewModel = new AccountViewModel()
                {
                    ID = account.ID,
                    Active = account.Active,
                    Balance = account.Balance,
                    CreatedDate = account.CreatedDate,
                    Number = account.Number,
                    BankID = account.BankID,
                    CustomerID = account.CustomerID                    
                };

                accountsViewModel.Add(accountViewModel);
            }

            return accountsViewModel;
        }

        public async Task<AccountViewModel> GetByIdAsync(Guid id)
        {
            var account = await _accountDomainService.GetByIdAsync(id);

            // todo: use automapper here
            return new AccountViewModel()
            {
                ID = account.ID,
                Active = account.Active,
                Balance = account.Balance,
                CreatedDate = account.CreatedDate,
                Number = account.Number,
                BankID = account.BankID,
                CustomerID = account.CustomerID
            };
        }

        public async Task<bool> UpdateAsync(AccountViewModel model)
        {
            var account = await _accountDomainService.GetByIdAsync(model.ID);

            account.Active = model.Active;

            return await _accountDomainService.UpdateAsync(account);
        }
    }
}
