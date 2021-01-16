using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Admin
{
    public class SystemGroupMemberShipConfig : BaseEntityConfig<SystemGroupMemberShip>, IEntityTypeConfiguration<SystemGroupMemberShip>
    {
        public override void Configure(EntityTypeBuilder<SystemGroupMemberShip> builder)
        {
            builder.ToTable("SystemGroupMemberShip", "app");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(50);

            builder.Property(e => e.OrganizationRoleId)
                .HasColumnName("OrganizationRoleId")
                .HasMaxLength(100);

            builder.Property(e => e.SystemGroupId)
                .HasColumnName("SystemGroupId")
                .HasMaxLength(50);

            builder.HasOne(d => d.OrganizationRole)
                .WithMany(p => p.SystemGroupMemberShips)
                .HasForeignKey(d => d.OrganizationRoleId)
                .HasConstraintName("FK_SystemGroupMemberShip_OrganizationRole");


            builder.HasOne(d => d.SystemGroup)
                .WithMany(p => p.SystemGroupMemberShips)
                .HasForeignKey(d => d.SystemGroupId)
                .HasConstraintName("FK_SystemGroupMemberShip_SystemGroup");


        }
    }
}
