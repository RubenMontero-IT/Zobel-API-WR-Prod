using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProductID")
                .HasMaxLength(100);

            builder.Property(e => e.Address)
                .HasMaxLength(256);

            builder.Property(e => e.BaseCurrencyId)
                .HasColumnName("BaseCurrency")
                .HasColumnType("char(3)")
                .IsUnicode(false);

            builder.Property(e => e.BloombergID)
                .HasMaxLength(100);

            builder.Property(e => e.BloombergName)
                .HasMaxLength(100);

            builder.Property(e => e.CUSIP)
                .HasMaxLength(100);

            builder.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .HasMaxLength(256);

            builder.Property(e => e.ISIN)
                .HasMaxLength(100);

            builder.Property(e => e.LastHistUpdate)
                .HasColumnType("date");

            builder.Property(e => e.ManagerName)
                .HasMaxLength(256);

            builder.Property(e => e.OtherID)
                    .HasMaxLength(256);

            builder.Property(e => e.RegionId)
                .HasColumnName("Region")
                .HasMaxLength(100);

            builder.Property(e => e.SEDOL)
                .HasMaxLength(100);

            builder.Property(e => e.StyleId)
                .HasColumnName("Style")
                .HasMaxLength(100);

            builder.Property(e => e.Ticker)
                .HasMaxLength(100);

            builder.Property(e => e.Type)
                .HasMaxLength(100);

            builder.HasOne(d => d.BaseCurrency)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.BaseCurrencyId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .HasConstraintName("FK_Product_Currency");

            builder.HasOne(d => d.Region)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Product_Region");

            builder.HasOne(d => d.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_Product_ProductType");

            builder.HasOne(d => d.Style)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Product_Style");
        }
    }
}
