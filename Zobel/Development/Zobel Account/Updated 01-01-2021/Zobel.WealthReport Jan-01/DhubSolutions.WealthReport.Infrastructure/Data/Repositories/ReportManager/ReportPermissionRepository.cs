using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager
{
    public class ReportPermissionRepository : Repository<ReportPermission>, IReportPermissionRepository
    {
        public ReportPermissionRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
