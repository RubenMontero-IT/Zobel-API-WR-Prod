using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportElementConfig : BaseReportElementConfig<ReportElement>
    {
        public override void Configure(EntityTypeBuilder<ReportElement> builder)
        {

            base.Configure(builder);

            builder.ToTable("ReportElement", "wrp");

            builder.Property(e => e.ReportId)
                .HasColumnName("ReportId");

            builder.Property(e => e.ContainerId)
                .HasColumnName("ContainerID");

            builder.HasOne(d => d.Report)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK_ReportElement_Report")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Container)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.ContainerId)
                .HasConstraintName("FK_ReportElement_ReportElement")
                /*.OnDelete(DeleteBehavior.Cascade)*/;

            builder.HasOne(d => d.ReportTemplateElement)
             .WithMany()
             .HasForeignKey(d => d.ReportTemplateElementId)
             .HasConstraintName("FK_ReportElement_ReportTemplateElement")
             .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
