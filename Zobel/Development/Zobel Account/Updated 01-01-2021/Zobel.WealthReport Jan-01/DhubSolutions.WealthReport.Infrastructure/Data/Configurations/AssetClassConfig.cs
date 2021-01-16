using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class AssetClassConfig : IEntityTypeConfiguration<AssetClass>
    {
        public void Configure(EntityTypeBuilder<AssetClass> builder)
        {
            builder.ToTable("AssetClass", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("AssetClassID")
                 .HasMaxLength(100);

            builder.Property(e => e.AssetClassName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
