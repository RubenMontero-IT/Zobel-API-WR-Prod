using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductFrequencyConfig : IEntityTypeConfiguration<ProductFrequency>
    {
        public void Configure(EntityTypeBuilder<ProductFrequency> builder)
        {
            builder.ToTable("ProductFrequency", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProductFrequencyID")
                .HasMaxLength(100);

            builder.HasIndex(e => new { e.FrequencyID, e.ProductID })
                .HasName("IX_ProductFrequency")
                .IsUnique();

            builder.Property(e => e.FrequencyID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Frequency)
                .WithMany(p => p.ProductFrequencies)
                .HasForeignKey(d => d.FrequencyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductFrequency_Frequency");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductFrequencies)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductFrequency_Product");
        }
    }
}
