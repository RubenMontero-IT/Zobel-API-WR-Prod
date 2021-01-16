using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class FullReportDto
    {
        public Organization Organization { get; set; }

        public Report Report { get; set; }
    }
}
