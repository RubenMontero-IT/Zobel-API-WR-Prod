using DhubSolutions.Common.Domain.Entities.Base;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportElementPermission : AssignableResource
    {
        public ReportElementPermission() : base()
        {
        }
        public string ReportElementId { get; set; }
        public ReportElement ReportElement { get; set; }
    }
}
