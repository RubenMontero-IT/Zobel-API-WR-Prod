using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductExtendedRegistryConfig : IEntityTypeConfiguration<ProductExtendedRegistry>
    {
        public void Configure(EntityTypeBuilder<ProductExtendedRegistry> builder)
        {
            builder.ToTable("ProductExtendedRegistry", "marketder");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProdExtendedRegistryID")
                .HasMaxLength(100);

            builder.HasIndex(e => new { e.ProductID, e.Date, e.MainCurrencyId })
                .HasName("IX_ProductExtendedRegistry")
                .IsUnique();

            builder.Property(e => e.BaseCurrencyId)
                .HasColumnName("BaseCurrency")
                .HasColumnType("char(3)")
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.BloombergID)
                .HasMaxLength(100);

            builder.Property(e => e.CUSIP)
                .HasMaxLength(100);

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.ISIN)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.MainCurrencyId)
                .HasColumnName("MainCurrency")
                .HasColumnType("char(3)")
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SEDOL)
                .HasMaxLength(100);

            builder.Property(e => e.Ticker)
                .HasMaxLength(100);

            builder.HasOne(d => d.BaseCurrency)
                   .WithMany(p => p.ProductExtendedRegistryBaseCurrencies)
                   .HasForeignKey(d => d.BaseCurrencyId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ProductExtendedRegistry_Currency1");

            builder.HasOne(d => d.MainCurrency)
                .WithMany(p => p.ProductExtendedRegistryMainCurrencies)
                .HasForeignKey(d => d.MainCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductExtendedRegistry_Currency");

            builder.HasOne(d => d.ProductRegistry)
                .WithMany(p => p.ProductExtendedRegistries)
                .HasPrincipalKey(p => new { p.ProductID, p.Date })
                .HasForeignKey(d => new { d.ProductID, d.Date })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductExtendedRegistry_ProductRegistry");
        }
    }
}
