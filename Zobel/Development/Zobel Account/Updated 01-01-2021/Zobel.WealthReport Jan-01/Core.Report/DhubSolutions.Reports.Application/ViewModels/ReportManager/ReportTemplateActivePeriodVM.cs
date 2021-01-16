using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportTemplateActivePeriodVM
    {
        public string Period { get; set; }

        public IEnumerable<PeriodStatusVM> PeriodStatuses { get; set; }
    }
}
