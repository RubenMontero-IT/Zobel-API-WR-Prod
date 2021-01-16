using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductRegistryConfig : IEntityTypeConfiguration<ProductRegistry>
    {
        public void Configure(EntityTypeBuilder<ProductRegistry> builder)
        {
            builder.ToTable("ProductRegistry", "invp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("ProductRegistryID")
            .HasMaxLength(100);

            builder.HasIndex(e => new { e.ProductID, e.Date })
                .HasName("IX_ProductRegistry")
                .IsUnique();

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductRegistries)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRegistry_Product");
        }
    }
}
