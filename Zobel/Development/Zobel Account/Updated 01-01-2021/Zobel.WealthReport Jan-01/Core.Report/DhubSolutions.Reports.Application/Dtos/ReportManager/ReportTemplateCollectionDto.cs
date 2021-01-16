using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class CollectionReportTemplateDto
    {
        public Organization Organization { get; set; }

        public IEnumerable<ReportTemplate> ReportTemplates { get; set; }
    }
}
