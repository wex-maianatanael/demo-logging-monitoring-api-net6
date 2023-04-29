using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;

namespace Demo.Infra.Repository.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly BankDbContext _dbContext;

        public AccountRepository(BankDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
