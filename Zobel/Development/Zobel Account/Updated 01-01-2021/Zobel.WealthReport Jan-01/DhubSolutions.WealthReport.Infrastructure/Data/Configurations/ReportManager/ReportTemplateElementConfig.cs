using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplateElementConfig : BaseReportElementConfig<ReportTemplateElement>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplateElement> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportTemplateElement", "wrp");

            builder.Property(e => e.ReportTemplateId)
                .HasColumnName("ReportTemplateId");

            builder.Property(e => e.ContainerId)
                .HasColumnName("ContainerId");

            //builder.Property(e => e.ElementIndex)
            //    .HasColumnName("ElementIndex")
            //    .ValueGeneratedOnAdd();

            builder.HasOne(d => d.ReportTemplate)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.ReportTemplateId)
                .HasConstraintName("FK_ReportTemplateElement_ReportTemplate")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Container)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.ContainerId)
                .HasConstraintName("FK_ReportTemplatetElement_ReportTemplateElement")
                /*.OnDelete(DeleteBehavior.Cascade)*/;
        }
    }
}
