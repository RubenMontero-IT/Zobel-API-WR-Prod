using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class FXRateConfig : IEntityTypeConfiguration<FXRate>
    {
        public void Configure(EntityTypeBuilder<FXRate> builder)
        {
            builder.ToTable("FXRate", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("FXRateID")
            .HasMaxLength(100);

            builder.HasIndex(e => new { e.Date, e.InitialCurrency, e.EndCurrency })
                .HasName("IX_FXRate")
                .IsUnique();

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.EndCurrency)
                .IsRequired()
                .HasColumnType("nchar(3)");

            builder.Property(e => e.InitialCurrency)
                .IsRequired()
                .HasColumnType("nchar(3)");
        }
    }
}
