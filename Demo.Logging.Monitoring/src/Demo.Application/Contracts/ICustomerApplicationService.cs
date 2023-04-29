using Demo.Application.ViewModels;

namespace Demo.Application.Contracts
{
    public interface ICustomerApplicationService
    {
        Task<CustomerViewModel> CreateAsync(CustomerViewModel model);
        Task<bool> UpdateAsync(CustomerViewModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<List<CustomerViewModel>> GetAllAsync();
        Task<CustomerViewModel> GetByIdAsync(Guid id);
    }
}
