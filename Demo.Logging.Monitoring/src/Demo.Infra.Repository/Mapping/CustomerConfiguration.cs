using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infra.Repository.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.HasOne(c => c.Account)
                .WithOne(a => a.Customer)
                .HasForeignKey<Account>(a => a.CustomerID);
        }
    }
}
