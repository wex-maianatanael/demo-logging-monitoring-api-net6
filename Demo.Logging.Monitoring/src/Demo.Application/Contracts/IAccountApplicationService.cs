using Demo.Application.ViewModels;

namespace Demo.Application.Contracts
{
    public interface IAccountApplicationService
    {
        Task<AccountViewModel> CreateAsync(AccountViewModel model);
        Task<bool> UpdateAsync(AccountViewModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<List<AccountViewModel>> GetAllAsync();
        Task<AccountViewModel> GetByIdAsync(Guid id);
    }
}
