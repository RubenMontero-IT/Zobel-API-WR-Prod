using DhubSolutions.Common.Domain.Entities.Base;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplateElementPermission : AssignableResource
    {
        public ReportTemplateElementPermission() : base()
        {

        }
        public string ReportTemplateElementId { get; set; }
        public ReportTemplateElement ReportTemplateElement { get; set; }
    }
}
