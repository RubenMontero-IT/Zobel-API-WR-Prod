using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportPermissionConfig : AssignableResourceConfig<ReportPermission>, IEntityTypeConfiguration<ReportPermission>
    {
        public override void Configure(EntityTypeBuilder<ReportPermission> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportPermission", "wrp");

            builder.Property(e => e.ReportId)
               .IsRequired()
               .HasColumnName("ReportID")
               .HasMaxLength(100);

            builder.Property(e => e.PermissionId)
                .IsRequired(false)
                .HasColumnName("PermissionID")
                .HasMaxLength(100);


            builder.Property(e => e.OrganizationId)
               .IsRequired()
               .HasColumnName("OrganizationID")
               .HasMaxLength(100);

            builder.HasIndex(e => new { e.OrganizationRoleId, e.OrganizationId, e.PermissionId, e.ReportId })
                .HasName("IX_ReportPermission")
                .IsUnique();

            builder.HasOne(e => e.Organization)
               .WithMany()
               .HasForeignKey(e => e.OrganizationId)
               .HasConstraintName("FK_ReportPermission_Organization");

            builder.HasOne(e => e.OrganizationRole)
                .WithMany()
                .HasForeignKey(e => e.OrganizationRoleId)
                .HasConstraintName("FK_ReportPermission_OrganizationRole");

            builder.HasOne(d => d.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_ReportPermission_Permission");

            builder.HasOne(d => d.Report)
                .WithMany(p => p.ReportPermissions)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK_ReportPermission_Report")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
