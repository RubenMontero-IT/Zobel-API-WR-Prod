using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currency", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("CurrencyID")
                 .HasColumnType("char(3)")
                 .IsUnicode(false);

            builder.Property(e => e.CurrencyName)
                 .IsRequired()
                 .HasMaxLength(50);
        }
    }
}
