using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportElementPermissionRepository :
        Repository<ReportElementPermission>, IReportElementPermissionRepository
    {
        public ReportElementPermissionRepository(ProjectManagementDbContext context, IEntityFrameworkUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
