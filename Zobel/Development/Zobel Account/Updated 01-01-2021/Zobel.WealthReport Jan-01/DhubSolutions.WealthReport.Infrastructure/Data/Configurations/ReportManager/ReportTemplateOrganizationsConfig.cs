using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplateOrganizationsConfig : BaseEntityConfig<ReportTemplateOrganization>, IEntityTypeConfiguration<ReportTemplateOrganization>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplateOrganization> builder)
        {
            builder.ToTable("ReportTemplateOrganizations", "wrp");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("ReportTemplateOrganizationID")
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
                .HasConstraintName("FK_ReportTemplateOrganization_Organization")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.ReportTemplate)
                .WithMany(p => p.TemplateOrganizations)
                .HasForeignKey(d => d.ReportTemplateId)
                .HasConstraintName("FK_ReportTemplateOrganization_ReportTemplate")
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
