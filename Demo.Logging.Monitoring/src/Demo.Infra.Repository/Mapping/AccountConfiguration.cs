using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infra.Repository.Mapping
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.ID);

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(a => a.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(a => a.Active)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(a => a.Balance)
                .IsRequired()
                .HasColumnType("decimal");
        }
    }
}
