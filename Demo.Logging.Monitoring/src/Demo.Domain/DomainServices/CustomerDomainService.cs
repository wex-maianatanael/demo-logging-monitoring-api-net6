using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Demo.Domain.DomainServices
{
    public class CustomerDomainService : BaseDomainService<Customer>, ICustomerDomainService
    {
        private readonly ILogger<CustomerDomainService> _loggerCustomerDomainService;
        private readonly ILogger<BaseDomainService<Customer>> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerDomainService(ICustomerRepository customerRepository, ILogger<CustomerDomainService> loggerCustomerDomainService, ILogger<BaseDomainService<Customer>> logger)
            : base(customerRepository, logger)
        {
            _customerRepository = customerRepository;
            _loggerCustomerDomainService = loggerCustomerDomainService;
        }
    }
}
