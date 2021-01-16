using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductStatusConfig : IEntityTypeConfiguration<ProductStatus>
    {
        public void Configure(EntityTypeBuilder<ProductStatus> builder)
        {
            builder.ToTable("ProductStatus", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("ProductStatusID")
                 .HasMaxLength(100);

            builder.Property(e => e.ProductStatusName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
