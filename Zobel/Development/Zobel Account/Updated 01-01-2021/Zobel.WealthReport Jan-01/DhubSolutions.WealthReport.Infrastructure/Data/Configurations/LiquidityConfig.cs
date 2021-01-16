using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class LiquidityConfig : IEntityTypeConfiguration<Liquidity>
    {
        public void Configure(EntityTypeBuilder<Liquidity> builder)
        {
            builder.ToTable("Liquidity", "market");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("LiquidityID")
                 .HasMaxLength(100);

            builder.Property(e => e.LiquidityValue).HasMaxLength(250);
        }
    }
}
