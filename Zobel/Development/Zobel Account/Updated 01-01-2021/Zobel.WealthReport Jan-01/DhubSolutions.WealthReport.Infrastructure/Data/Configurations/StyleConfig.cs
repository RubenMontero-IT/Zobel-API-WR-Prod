using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class StyleConfig : IEntityTypeConfiguration<Style>
    {
        public void Configure(EntityTypeBuilder<Style> builder)
        {
            builder.ToTable("Style", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("StyleID")
                .HasMaxLength(100);

            builder.Property(e => e.StyleName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
