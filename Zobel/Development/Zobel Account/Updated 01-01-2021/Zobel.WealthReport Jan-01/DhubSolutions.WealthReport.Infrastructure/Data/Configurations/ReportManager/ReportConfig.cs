using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportConfig : BaseReportConfig<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);

            builder.ToTable("Report", "wrp");

            builder.Property(e => e.CreationOptions)
                .HasColumnName("CreationOptions")
                .IsRequired();

            builder.Property(e => e.PeriodId)
                .HasColumnName("PeriodID")
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(e => e.TemplateId)
                .HasColumnName("TemplateId");

            builder.HasOne(d => d.Template)
                .WithMany()
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK_Report_ReportTemplate")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .HasConstraintName("FK_Report_User");


        }
    }
}
