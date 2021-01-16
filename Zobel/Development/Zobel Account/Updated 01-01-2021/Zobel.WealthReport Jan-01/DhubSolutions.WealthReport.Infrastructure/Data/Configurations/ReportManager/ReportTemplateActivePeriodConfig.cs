using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplateActivePeriodConfig : BaseEntityConfig<ReportTemplateActivePeriod>, IEntityTypeConfiguration<ReportTemplateActivePeriod>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplateActivePeriod> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportTemplateActivePeriod", "wrp");

            builder.Property(e => e.Period)
                .HasColumnName("Period")
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasColumnName("IsActive");

            builder.Property(e => e.ReportTemplateId)
                .HasColumnName("ReportTemplateId")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.OrganizationId)
                .HasColumnName("OrganizationId")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.ReportTemplate)
                .WithMany(p => p.ActivePeriods)
                .HasForeignKey(d => d.ReportTemplateId)
                .HasConstraintName("FK_ReportTemplateActivePeriod_ReportTemplate")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Organization)
                .WithMany()
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_ReportTemplateActivePeriod_Organization")
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
