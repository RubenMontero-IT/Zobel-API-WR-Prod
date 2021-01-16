using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplateElementPermissionConfig : AssignableResourceConfig<ReportTemplateElementPermission>, IEntityTypeConfiguration<ReportTemplateElementPermission>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplateElementPermission> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportTemplateElementPermission", "wrp");

            builder.HasIndex(e => new { e.OrganizationRoleId, e.PermissionId, e.ReportTemplateElementId })
                .HasName("IX_ReportTemplateElementPermission")
                .IsUnique();

            builder.Property(e => e.PermissionId)
                .IsRequired()
                .HasColumnName("PermissionID")
                .HasMaxLength(100);

            builder.Property(e => e.ReportTemplateElementId)
               .IsRequired()
               .HasColumnName("ReportTemplateElementID")
               .HasMaxLength(100);

            builder.HasOne(e => e.OrganizationRole)
                .WithMany()
                .HasForeignKey(e => e.OrganizationRoleId)
                .HasConstraintName("FK_ReportTemplateElementPermission_OrganizationRole");

            builder.HasOne(e => e.ReportTemplateElement)
                .WithMany(p => p.ReportTemplateElementPermissions)
                .HasForeignKey(e => e.ReportTemplateElementId)
                .HasConstraintName("FK_ReportTemplateElementPermission_ReportTemplateElement")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Permission)
               .WithMany()
               .HasForeignKey(d => d.PermissionId)
               .HasConstraintName("FK_ReportTemplateElementPermission_Permission");
        }
    }
}
