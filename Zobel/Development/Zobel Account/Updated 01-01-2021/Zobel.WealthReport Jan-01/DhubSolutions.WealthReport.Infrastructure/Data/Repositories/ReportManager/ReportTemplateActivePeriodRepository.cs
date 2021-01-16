using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportTemplateActivePeriodRepository : Repository<ReportTemplateActivePeriod>, IReportTemplateActivePeriodRepository
    {
        public ReportTemplateActivePeriodRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
