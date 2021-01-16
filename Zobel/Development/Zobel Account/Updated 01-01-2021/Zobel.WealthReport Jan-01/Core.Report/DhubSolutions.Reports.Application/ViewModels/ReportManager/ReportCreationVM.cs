using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportCreationVM
    {
        public string TemplateId { get; set; }

        public string OrganizationId { get; set; }

        public string ReportName { get; set; }

        public Dictionary<string, dynamic> Params { get; set; }
    }
}
