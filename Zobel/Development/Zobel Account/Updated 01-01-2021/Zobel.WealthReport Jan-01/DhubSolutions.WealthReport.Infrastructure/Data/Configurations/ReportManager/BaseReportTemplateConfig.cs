using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Base;
using DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public abstract class BaseReportTemplateConfig<T> : BaseReportConfig<T> where T : class, IBaseReport, IEntity, new()
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.ToTable("ReportTemplate", "wrp");

            builder.HasOne(e => e.CreatedBy)
                   .WithMany()
                   .HasForeignKey(e => e.CreatedById)
                   .HasConstraintName("FK_ReportTemplate_User");
        }
    }
}
