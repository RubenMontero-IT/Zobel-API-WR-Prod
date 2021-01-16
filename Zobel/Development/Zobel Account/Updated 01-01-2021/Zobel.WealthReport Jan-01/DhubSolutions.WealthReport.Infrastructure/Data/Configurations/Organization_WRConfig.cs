using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class Organization_WRConfig : IEntityTypeConfiguration<Organization_WR>
    {
        public void Configure(EntityTypeBuilder<Organization_WR> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Organization", "userm");

            builder.Property(e => e.Id)
                .HasColumnName("ORGID")
                .HasMaxLength(100)
                .ValueGeneratedNever();

            builder.Property(e => e.LucanetId)
                .HasColumnName("LucanetId");

            builder.Property(e => e.OrganizationDescription)
                .HasMaxLength(150);

            builder.Property(e => e.OrganizationName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ResourceId)
                .HasColumnName("ResourceID")
                .HasMaxLength(100);

            builder.Property(e => e.CurrencyId)
                .HasColumnName("CurrencyID")
                .HasColumnType("char(3)");

        }
    }
}
