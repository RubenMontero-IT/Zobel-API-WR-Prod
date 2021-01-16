using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplateOrganization : BaseEntity
    {
        public ReportTemplateOrganization() : base()
        {

        }

        public string ReportTemplateId { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }



}
