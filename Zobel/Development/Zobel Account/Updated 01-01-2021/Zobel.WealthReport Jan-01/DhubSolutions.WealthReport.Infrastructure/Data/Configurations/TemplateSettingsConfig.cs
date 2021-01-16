using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations
{
    public class TemplateSettingsConfig : BaseEntityConfig<TemplateSettings>
    {
        public override void Configure(EntityTypeBuilder<TemplateSettings> builder)
        {
            base.Configure(builder);

            builder.ToTable("TemplateSettings", "wrp");

            builder.Property(e => e.OrganizationId)
                .HasColumnName("ORGID")
                .IsRequired();

            builder.Property(e => e.UserRoleOrgId)
                .HasColumnName("UserRoleOrgID")
                .IsRequired();

            builder.Property(e => e.TemplateCode)
                .HasColumnName("TemplateCode")
                .IsRequired();

            builder.Property(e => e.Settings)
                .HasColumnName("Settings")
                .IsRequired();

            builder.HasOne(d => d.Organization)
                .WithMany()
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_TemplateSettings_Organization");

            builder.HasOne(d => d.UserRoleOrg)
                .WithMany()
                .HasForeignKey(d => d.UserRoleOrgId)
                .HasConstraintName("FK_TemplateSettings_UserRoleOrg");

            builder.HasOne(d => d.ReportTemplate)
                .WithMany()
                .HasForeignKey(d => d.TemplateCode)
                .HasConstraintName("FK_TemplateSettings_ReportTemplate");
        }
    }
}
