using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class OrganizationProductConfig : IEntityTypeConfiguration<OrganizationProduct>
    {
        public void Configure(EntityTypeBuilder<OrganizationProduct> builder)
        {
            builder.ToTable("OrganizationProduct", "invp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("OrgProductID")
                .HasMaxLength(100);

            builder.HasIndex(e => new { e.ProductID, e.OrganizationID })
                .HasName("IX_OrganizationProduct")
                .IsUnique();

            builder.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.InitialDate).HasColumnType("date");

            builder.Property(e => e.OrganizationID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrganizationProducts)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrganizationProduct_Product");
        }
    }
}
