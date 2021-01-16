using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class MetricsCategoriesConfig : IEntityTypeConfiguration<MetricsCategories>
    {
        public void Configure(EntityTypeBuilder<MetricsCategories> builder)
        {
            builder.ToTable("MetricsCategories", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("ID")
                 .HasMaxLength(100);

            builder.Property(e => e.DisplayName)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(e => e.MetricCode)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(e => e.Period)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(e => e.Plevel)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(e => e.Type)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
