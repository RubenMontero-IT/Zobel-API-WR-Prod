using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplatePermissionConfig : AssignableResourceConfig<ReportTemplatePermission>, IEntityTypeConfiguration<ReportTemplatePermission>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplatePermission> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportTemplatePermission", "wrp");

            //builder.HasIndex(e => new { e.OrganizationRoleId, e.PermissionId, e.ReportTemplateId })
            //    .HasName("IX_ReportTemplatePermission")
            //    .IsUnique();

            builder.Property(e => e.PermissionId)
                //.IsRequired()
                .HasColumnName("PermissionID")
                .HasMaxLength(100);

            builder.Property(e => e.OrganizationId)
                .IsRequired()
                .HasColumnName("OrganizationID")
                .HasMaxLength(100);

            builder.Property(e => e.ReportTemplateId)
               .IsRequired()
               .HasColumnName("ReportTemplateID")
               .HasMaxLength(100);

            builder.HasOne(e => e.Organization)
                .WithMany()
                .HasForeignKey(e => e.OrganizationId)
                .HasConstraintName("FK_ReportTemplatePermission_Organization")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.OrganizationRole)
                .WithMany()
                .HasForeignKey(e => e.OrganizationRoleId)
                .HasConstraintName("FK_ReportTemplatePermission_OrganizationRole");

            builder.HasOne(d => d.ReportTemplate)
                .WithMany(p => p.ReportTemplatePermissions)
                .HasForeignKey(d => d.ReportTemplateId)
                .HasConstraintName("FK_ReportTemplatePermission_ReportTemplate")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_ReportTemplatePermission_Permission");
        }
    }
}
