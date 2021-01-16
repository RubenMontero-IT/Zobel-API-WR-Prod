using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Configurations.ReportManager
{
    public class ReportTemplateConfig : BaseReportTemplateConfig<ReportTemplate>
    {
        public override void Configure(EntityTypeBuilder<ReportTemplate> builder)
        {
            base.Configure(builder);

            //builder.ToTable("WealthReportTemplate", "wrp");
        }
    }
}
