using Demo.Application.ApplicationServices;
using Demo.Application.Contracts;
using Demo.Domain.Contracts.DomainServices;
using Demo.Domain.Contracts.Repositories;
using Demo.Domain.DomainServices;
using Demo.Infra.Repository.Repositories;

namespace Demo.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionResolver(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBankApplicationService, BankApplicationService>();
            builder.Services.AddScoped<IBankDomainService, BankDomainService>();
            builder.Services.AddScoped<IBankRepository, BankRepository>();

            builder.Services.AddScoped<IAccountApplicationService, AccountApplicationService>();
            builder.Services.AddScoped<IAccountDomainService, AccountDomainService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            builder.Services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();
            builder.Services.AddScoped<ICustomerDomainService, CustomerDomainService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            return builder;
        }
    }
}
