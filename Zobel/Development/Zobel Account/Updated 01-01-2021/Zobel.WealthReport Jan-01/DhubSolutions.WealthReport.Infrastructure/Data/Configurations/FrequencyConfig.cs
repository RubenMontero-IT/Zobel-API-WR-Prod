using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class FrequencyConfig : IEntityTypeConfiguration<Frequency>
    {
        public void Configure(EntityTypeBuilder<Frequency> builder)
        {
            builder.ToTable("Frequency", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("FrequencyID")
                 .HasMaxLength(100);

            builder.Property(e => e.FrequencyName)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
