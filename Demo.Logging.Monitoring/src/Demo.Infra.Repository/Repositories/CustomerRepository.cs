using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;

namespace Demo.Infra.Repository.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly BankDbContext _dbContext;

        public CustomerRepository(BankDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
