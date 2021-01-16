using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class SectionConfig : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Section", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("SectionID")
                 .HasMaxLength(100);

            builder.Property(e => e.SectionName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
