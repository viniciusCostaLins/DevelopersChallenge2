using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nibo.Domain.Entities.Accounts;

namespace Nibo.Repositories.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(p => p.AccountId).HasColumnType("varchar(255)");
            builder.Property(p => p.AccountType).HasColumnType("varchar(255)");
            builder.Property(p => p.BankId).HasColumnType("varchar(255)");                        

            builder.HasKey(n => n.Id);

            builder.HasMany(t => t.Transactions)
                .WithOne(a => a.Account)
                .HasForeignKey(t => t.AccountId);

            builder.ToTable("Account");
        }
    }
}
