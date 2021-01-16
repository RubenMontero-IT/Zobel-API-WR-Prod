using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class CollectionOrganizationDto
    {
        public ReportTemplate ReportTemplate { get; set; }
        public IEnumerable<Organization> Organizations { get; set; }
    }
}
