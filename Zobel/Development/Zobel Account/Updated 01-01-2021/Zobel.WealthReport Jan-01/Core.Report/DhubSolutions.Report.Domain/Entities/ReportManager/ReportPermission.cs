using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Base;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportPermission : AssignableResource
    {
        public ReportPermission() : base()
        {
        }

        public string ReportId { get; set; }

        public Report Report { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
