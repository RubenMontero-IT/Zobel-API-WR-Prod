using DhubSolutions.Common.Domain.Entities.Admin;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportCreationDto
    {
        public ReportTemplateDto ReportTemplate { get; set; }

        public string ReportName { get; set; }

        public OrganizationRole UserOrganizationRole { get; set; }

        public Organization Organization { get; set; }

        public User User { get; set; }

        public Dictionary<string, dynamic> Params { get; set; }
    }
}
