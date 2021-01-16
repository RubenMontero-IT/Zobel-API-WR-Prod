using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ProductStyleConfig : IEntityTypeConfiguration<ProductStyle>
    {
        public void Configure(EntityTypeBuilder<ProductStyle> builder)
        {
            builder.ToTable("ProductStyle", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProductStyleID")
                .HasMaxLength(100);

            builder.Property(e => e.ProductStyleName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
