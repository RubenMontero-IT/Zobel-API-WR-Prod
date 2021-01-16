using System.Collections.Generic;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public interface IBaseReportTemplate
    {
        string Code { get; set; }
        ICollection<ReportTemplateElement> Content { get; set; }
    }
}