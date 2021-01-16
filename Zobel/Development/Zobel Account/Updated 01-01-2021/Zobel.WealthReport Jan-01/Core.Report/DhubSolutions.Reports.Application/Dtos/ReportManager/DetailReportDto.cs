using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class DetailReportDto
    {
        public OrganizationRole OrganizationRole { get; set; }

        public Report Report { get; set; }
    }
}
