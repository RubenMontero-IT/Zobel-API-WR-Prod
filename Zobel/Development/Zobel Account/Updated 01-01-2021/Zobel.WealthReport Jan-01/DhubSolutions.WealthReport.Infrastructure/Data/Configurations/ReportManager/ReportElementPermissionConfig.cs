using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportElementPermissionConfig : AssignableResourceConfig<ReportElementPermission>, IEntityTypeConfiguration<ReportElementPermission>
    {
        public override void Configure(EntityTypeBuilder<ReportElementPermission> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportElementPermission", "wrp");

            builder.Property(e => e.PermissionId)
                .IsRequired()
                .HasColumnName("PermissionID")
                .HasMaxLength(100);


            builder.HasIndex(e => new { e.OrganizationRoleId, e.PermissionId, e.ReportElementId })
                .HasName("IX_ReportElementPermission")
                .IsUnique();

            builder.Property(e => e.ReportElementId)
                .IsRequired()
                .HasColumnName("ReportElementID")
                .HasMaxLength(100);

            builder.HasOne(e => e.OrganizationRole)
                .WithMany()
                .HasForeignKey(e => e.OrganizationRoleId)
                .HasConstraintName("FK_ReportElementPermission_OrganizationRole");

            builder.HasOne(d => d.ReportElement)
                .WithMany(p => p.ReportElementPermissions)
                .HasForeignKey(d => d.ReportElementId)
                .HasConstraintName("FK_ReportElementPermission_ReportElement")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_ReportElementPermission_Permission");
        }
    }
}
