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
            
            // todo: use automapper here
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

            if (banks.Count == 0 || banks == null)
            {
                return null;
            }

            // todo: use automapper here
            var banksViewModel = new List<BankViewModel>();
            foreach (var bank in banks)
            {
                var bankViewModel = new BankViewModel()
                {
                    ID = bank.ID,
                    Name = bank.Name
                };

                banksViewModel.Add(bankViewModel);
            }

            return banksViewModel;
        }

        public async Task<BankViewModel> GetByIdAsync(Guid id)
        {
            var bank = await _bankDomainService.GetByIdAsync(id);

            // todo: use automapper here
            return new BankViewModel()
            {
                ID = bank.ID,
                Name = bank.Name
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
