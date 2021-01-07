using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nibo.Domain.Entities.Transactions;

namespace Nibo.Repositories.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(p => p.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.DatePosted).HasColumnType("datetime2(7)");
            builder.Property(p => p.Memo).HasColumnType("varchar(500)");
            builder.Property(p => p.TransactionType).HasColumnType("varchar(255)");
            builder.Property(p => p.AccountId).IsRequired(false);

            builder.HasKey(n => n.Id);            

            builder.ToTable("Transaction");
        }
    }
}
