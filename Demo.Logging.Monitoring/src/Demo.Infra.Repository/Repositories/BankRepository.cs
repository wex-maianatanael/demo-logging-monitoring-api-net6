using Demo.Domain.Contracts.Repositories;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Context;

namespace Demo.Infra.Repository.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        private readonly BankDbContext _dbContext;

        public BankRepository(BankDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
