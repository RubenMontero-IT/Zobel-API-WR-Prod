using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.ReportManager;

namespace DhubSolutions.Reports.Domain.Repositories.ReportManager
{
    public interface IReportRepository : IRepository<Report>
    {

        string CloneReport(Report report);

        string GetSettings(OrganizationRole organizationRole);
    }
}
