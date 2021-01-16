using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;

namespace DhubSolutions.Reports.Application.Services.ReportManager
{
    public class ReportTemplateActivePeriodService : ServiceMapper<ReportTemplateActivePeriod>, IReportTemplateActivePeriodService
    {
        public ReportTemplateActivePeriodService(
            IUnitOfWork unitOfWork,
            ITypeAdapter typeAdapter,
            IReportTemplateActivePeriodRepository repository) : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
