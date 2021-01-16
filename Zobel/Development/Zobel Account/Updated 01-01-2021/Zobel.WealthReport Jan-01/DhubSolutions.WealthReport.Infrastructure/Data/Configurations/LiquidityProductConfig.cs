using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class LiquidityProductConfig : IEntityTypeConfiguration<LiquidityProduct>
    {
        public void Configure(EntityTypeBuilder<LiquidityProduct> builder)
        {
            builder.ToTable("LiquidityProduct", "market");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.ProductID, e.LiquidityID })
                 .HasName("IX_LiquidityProduct")
                 .IsUnique();

            builder.Property(e => e.Id)
                 .HasColumnName("LiquidityProductID")
                 .HasMaxLength(100);

            builder.Property(e => e.LiquidityID)
                 .IsRequired()
                 .HasColumnName("LiquidityID")
                 .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                 .IsRequired()
                 .HasColumnName("ProductID")
                 .HasMaxLength(100);

            builder.HasOne(d => d.Liquidity)
                 .WithMany(p => p.LiquidityProducts)
                 .HasForeignKey(d => d.LiquidityID)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_LiquidityProduct_Liquidity");

            builder.HasOne(d => d.Product)
                 .WithMany(p => p.LiquidityProducts)
                 .HasForeignKey(d => d.ProductID)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_LiquidityProduct_Product");
        }
    }
}
