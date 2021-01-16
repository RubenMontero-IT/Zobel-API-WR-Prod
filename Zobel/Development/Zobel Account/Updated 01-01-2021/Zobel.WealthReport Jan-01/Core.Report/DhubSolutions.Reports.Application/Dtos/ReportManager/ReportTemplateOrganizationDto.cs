using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateOrganizationDto
    {
        public ReportTemplate ReportTemplate { get; set; }

        public Organization Organization { get; set; }
    }
}
