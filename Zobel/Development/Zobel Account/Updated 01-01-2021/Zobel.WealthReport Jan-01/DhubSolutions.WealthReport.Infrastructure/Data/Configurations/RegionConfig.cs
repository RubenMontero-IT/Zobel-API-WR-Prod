using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("RegionID")
                .HasMaxLength(100);

            builder.Property(e => e.RegionName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
