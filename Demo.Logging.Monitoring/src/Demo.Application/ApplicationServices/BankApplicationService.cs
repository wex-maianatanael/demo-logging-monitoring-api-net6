using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Entities;

namespace Demo.Application.ApplicationServices
{
    public class BankApplicationService : IBankApplicationService
    {
        private readonly IBankDomainService _bankDomainService;

        public BankApplicationService(IBankDomainService bankDomainService)
        {
            _bankDomainService = bankDomainService;
        }

        public async Task<BankViewModel> CreateAsync(BankViewModel model)
        {
            var bank = new Bank(){ Name = model.Name };

            var createdBank = await _bankDomainService.CreateAsync(bank);

            return new BankViewModel { ID = createdBank.ID, Name = createdBank.Name };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var bank = await _bankDomainService.GetByIdAsync(id);

            return await _bankDomainService.DeleteAsync(bank);
        }

        public async Task<List<BankViewModel>> GetAllAsync()
        {
            var banks = await _bankDomainService.GetAllAsync();

            var response = new List<BankViewModel>();
            var accounts = new List<AccountViewModel>();
            foreach (var bank in banks)
            {
                var model = new BankViewModel()
                {
                    ID = bank.ID,
                    Name = bank.Name
                };

                foreach (var account in bank.Accounts)
                {
                    var accountViewModel = new AccountViewModel()
                    {
                        ID = account.ID,
                        Number = account.Number,
                        CreatedDate = account.CreatedDate,
                        Active = account.Active,
                        Balance = account.Balance,
                        BankID = account.BankID,
                        CustomerID = account.CustomerID                        
                    };

                    model.Accounts.Add(accountViewModel);
                }

                response.Add(model);
            }

            return response;
        }

        public async Task<BankViewModel> GetByIdAsync(Guid id)
        {
            var bank = await _bankDomainService.GetByIdAsync(id);

            var accountsModel = new List<AccountViewModel>();
            foreach (var account in bank.Accounts)
            {
                var accountModel = new AccountViewModel()
                {
                    ID = account.ID,
                    CustomerID = account.CustomerID,
                    BankID = account.BankID,
                    CreatedDate = account.CreatedDate,
                    Active = account.Active,
                    Balance = account.Balance,
                    Number = account.Number
                };

                accountsModel.Add(accountModel);
            }

            return new BankViewModel()
            {
                ID = bank.ID,
                Name = bank.Name,
                Accounts = accountsModel
            };
        }

        public async Task<bool> UpdateAsync(BankViewModel model)
        {
            var bank = await _bankDomainService.GetByIdAsync(model.ID);

            bank.Name = model.Name;

            return await _bankDomainService.UpdateAsync(bank);
        }
    }
}
