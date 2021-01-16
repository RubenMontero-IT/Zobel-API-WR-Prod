using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class OrganizationProductRegistryConfig : IEntityTypeConfiguration<OrganizationProductRegistry>
    {
        public void Configure(EntityTypeBuilder<OrganizationProductRegistry> builder)
        {
            builder.ToTable("OrganizationProductRegistry", "invp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("OrgProductRegistryID")
                .HasMaxLength(100);

            builder.HasIndex(e => new { e.ProductID, e.OrganizationID, e.Date })
                .HasName("IX_OrganizationProductRegistry")
                .IsUnique();

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.OrganizationID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ProductID)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.OrganizationProduct)
                .WithMany(p => p.OrganizationProductRegistries)
                .HasPrincipalKey(p => new { p.ProductID, p.OrganizationID })
                .HasForeignKey(d => new { d.ProductID, d.OrganizationID })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrganizationProductRegistry_OrganizationProduct");
        }
    }
}
