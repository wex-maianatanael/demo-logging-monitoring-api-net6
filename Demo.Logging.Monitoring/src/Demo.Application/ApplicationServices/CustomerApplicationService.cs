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

        public CustomerApplicationService(ICustomerDomainService customerDomainService)
        {
            _customerDomainService = customerDomainService;
        }

        public async Task<CustomerViewModel> CreateAsync(CustomerViewModel model)
        {
            var customer = new Customer() { Name = model.Name, BirthDate = model.BirthDate };
            var createdCustomer = await _customerDomainService.CreateAsync(customer);

            return new CustomerViewModel { ID = createdCustomer.ID, Name = createdCustomer.Name, BirthDate = createdCustomer.BirthDate };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _customerDomainService.GetByIdAsync(id);

            return await _customerDomainService.DeleteAsync(customer);
        }

        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            var customers = await _customerDomainService.GetAllAsync();

            // todo: use automapper here
            var customersViewModel = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                var customerViewModel = new CustomerViewModel()
                {
                    ID = customer.ID,
                    Name = customer.Name,
                    BirthDate = customer.BirthDate
                };

                customersViewModel.Add(customerViewModel);
            }

            return customersViewModel;
        }

        public async Task<CustomerViewModel> GetByIdAsync(Guid id)
        {
            var customer = await _customerDomainService.GetByIdAsync(id);

            // todo: use automapper here
            return new CustomerViewModel()
            {
                ID = customer.ID,
                Name = customer.Name,
                BirthDate = customer.BirthDate
            };
        }

        public async Task<bool> UpdateAsync(CustomerViewModel model)
        {
            var customer = await _customerDomainService.GetByIdAsync(model.ID);

            customer.Name = model.Name;
            customer.BirthDate = model.BirthDate;

            return await _customerDomainService.UpdateAsync(customer);
        }
    }
}
