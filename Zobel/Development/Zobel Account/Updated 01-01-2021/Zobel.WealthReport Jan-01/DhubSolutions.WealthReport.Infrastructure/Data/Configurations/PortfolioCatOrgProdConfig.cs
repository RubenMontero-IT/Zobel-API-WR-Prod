using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class PortfolioCatOrgProdConfig : IEntityTypeConfiguration<PortfolioCatOrgProd>
    {
        public void Configure(EntityTypeBuilder<PortfolioCatOrgProd> builder)
        {
            builder.ToTable("PortfolioCatOrgProd", "invp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .HasColumnName("PortfolioCatOrgProdID")
            .HasMaxLength(100);

            builder.HasIndex(e => new { e.PortfolioCategoryID, e.ProductID, e.OrganizationID })
                .HasName("IX_PortfolioCatOrgProd")
                .IsUnique();

            builder.Property(e => e.OrganizationID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PortfolioCategoryID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.PortfolioCategory)
                .WithMany(p => p.PortfolioCatOrgProds)
                .HasForeignKey(d => d.PortfolioCategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioCatOrgProd_PorfolioCategory");

            builder.HasOne(d => d.OrganizationProduct)
                .WithMany(p => p.PortfolioCatOrgProds)
                .HasPrincipalKey(p => new { p.ProductID, p.OrganizationID })
                .HasForeignKey(d => new { d.ProductID, d.OrganizationID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioCatOrgProd_OrganizationProduct");
        }
    }
}
