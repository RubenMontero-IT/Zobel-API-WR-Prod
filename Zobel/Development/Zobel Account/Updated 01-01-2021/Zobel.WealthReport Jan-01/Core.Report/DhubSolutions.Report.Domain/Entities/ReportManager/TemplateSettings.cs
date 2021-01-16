using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class TemplateSettings : BaseEntity
    {
        public TemplateSettings() : base()
        { }

        public string UserRoleOrgId { get; set; }

        public UserRoleOrg UserRoleOrg { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public string TemplateCode { get; set; }

        public string Settings { get; set; }
    }
}
