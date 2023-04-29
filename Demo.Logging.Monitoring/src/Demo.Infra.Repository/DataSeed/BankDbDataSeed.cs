using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infra.Repository.DataSeed
{
    public static class BankDbDataSeed
    {
        private static Guid bankID_1 = Guid.NewGuid();
        private static Guid customerID_1 = Guid.NewGuid();
        private static Guid customerID_2 = Guid.NewGuid();
        private static Guid customerID_3 = Guid.NewGuid();

        public static ModelBuilder StartDataSeed(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Bank>().HasData(
                new Bank() { ID = bankID_1, Name = "Bank 1" }
                );

            modelBuilder.Entity<Account>().HasData(
                new Account() { ID = Guid.NewGuid(), Number = 111111, Active = true, Balance = 1000M, CreatedDate = DateTime.Now, BankID = bankID_1, CustomerID = customerID_1 },
                new Account() { ID = Guid.NewGuid(), Number = 222222, Active = true, Balance = 1200M, CreatedDate = DateTime.Now, BankID = bankID_1, CustomerID = customerID_2 },
                new Account() { ID = Guid.NewGuid(), Number = 333333, Active = true, Balance = 2300M, CreatedDate = DateTime.Now, BankID = bankID_1, CustomerID = customerID_3 }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { ID = customerID_1, BirthDate = new DateTime(1986,1,10).Date, Name = "Kyle" },
                new Customer() { ID = customerID_2, BirthDate = new DateTime(1998,3,15).Date, Name = "Alice" },
                new Customer() { ID = customerID_3, BirthDate = new DateTime(2000,6,25).Date, Name = "Bob" }
                );

            return modelBuilder;
        }
    }
}
