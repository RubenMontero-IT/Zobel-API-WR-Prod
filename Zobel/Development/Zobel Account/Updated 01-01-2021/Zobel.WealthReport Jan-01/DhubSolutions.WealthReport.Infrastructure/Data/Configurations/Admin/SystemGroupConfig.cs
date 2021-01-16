using DhubSolutions.Common.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Admin
{
    public class SystemGroupConfig : IEntityTypeConfiguration<SystemGroup>
    {
        public void Configure(EntityTypeBuilder<SystemGroup> builder)
        {

            builder.ToTable("SystemGroup", "app");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(50);

            builder.Property(e => e.Code)
                .HasColumnName("Code")
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(100);


        }
    }
}
