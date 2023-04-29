using Demo.Application.ViewModels;

namespace Demo.Application.Contracts
{
    public interface IBankApplicationService
    {
        Task<BankViewModel> CreateAsync(BankViewModel model);
        Task<bool> UpdateAsync(BankViewModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<List<BankViewModel>> GetAllAsync();
        Task<BankViewModel> GetByIdAsync(Guid id);
    }
}
