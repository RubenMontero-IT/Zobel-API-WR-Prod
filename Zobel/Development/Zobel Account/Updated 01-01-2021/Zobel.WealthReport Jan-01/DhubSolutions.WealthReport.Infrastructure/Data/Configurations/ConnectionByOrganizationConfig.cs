using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class ConnectionByOrganizationConfig : IEntityTypeConfiguration<ConnectionByOrganization>
    {
        public void Configure(EntityTypeBuilder<ConnectionByOrganization> builder)
        {
            builder.ToTable("ConnectionByOrganization", "wrp");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ORGID")
                .HasMaxLength(100);

            builder.Property(e => e.ConnectionID)
                .IsRequired()
                .HasColumnName("ConnectionID")
                .HasMaxLength(100);

        }
    }
}
