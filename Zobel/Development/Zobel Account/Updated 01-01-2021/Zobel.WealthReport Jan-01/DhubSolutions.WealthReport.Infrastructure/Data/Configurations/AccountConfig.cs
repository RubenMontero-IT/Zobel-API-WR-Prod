using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "invp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("AccountID")
                 .HasMaxLength(100);

            builder.Property(e => e.AccountName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
