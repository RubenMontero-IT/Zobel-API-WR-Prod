using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class PorfolioCategoryConfig : IEntityTypeConfiguration<PorfolioCategory>
    {
        public void Configure(EntityTypeBuilder<PorfolioCategory> builder)
        {
            builder.ToTable("PorfolioCategory", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("PortfolioCategoryID")
            .HasMaxLength(100);

            builder.Property(e => e.PortfolioCategory)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
