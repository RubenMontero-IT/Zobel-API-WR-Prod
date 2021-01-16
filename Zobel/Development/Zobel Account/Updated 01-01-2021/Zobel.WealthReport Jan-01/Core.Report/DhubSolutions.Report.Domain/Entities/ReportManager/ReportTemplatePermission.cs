using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Base;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplatePermission : AssignableResource
    {
        public ReportTemplatePermission() : base()
        {

        }

        public string ReportTemplateId { get; set; }
        public ReportTemplate ReportTemplate { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
