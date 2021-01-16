using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class PortfolioCategoryConfig : IEntityTypeConfiguration<PortfolioCategory>
    {
        public void Configure(EntityTypeBuilder<PortfolioCategory> builder)
        {
            builder.ToTable("PortfolioCategory", "gral");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("PortfolioCategoryID")
            .HasMaxLength(100);

            builder.Property(e => e.PortfolioCategoryName)
                .IsRequired()
                .HasColumnName("PortfolioCategory")
                .HasMaxLength(100);

            builder.Property(e => e.BaseCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

            builder.Property(e => e.CategoryCode).HasMaxLength(100);

            builder.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'General Portfolio')");

            builder.Property(e => e.Employees).HasMaxLength(150);

            builder.Property(e => e.FirmAUM)
                      .HasMaxLength(150);

            builder.Property(e => e.FundAUM)                    
                    .HasMaxLength(150);

            builder.Property(e => e.LiquidityID)                               
                    .HasMaxLength(100);

            builder.Property(e => e.ManagementFee).HasMaxLength(100);

            builder.Property(e => e.PerformanceFee).HasMaxLength(100);

            builder.Property(e => e.PortfolioManager).HasMaxLength(250);

            builder.Property(e => e.ProductStyleID)                               
                                .HasMaxLength(100);

            builder.Property(e => e.RegisteredOffice).HasMaxLength(150);

            builder.Property(e => e.VisibleDetails)
                                .IsRequired()
                                .HasDefaultValueSql("((1))");

            builder.Property(e => e.VisibleFacts)
                                .IsRequired()
                                .HasDefaultValueSql("((1))");
        }
    }
}



