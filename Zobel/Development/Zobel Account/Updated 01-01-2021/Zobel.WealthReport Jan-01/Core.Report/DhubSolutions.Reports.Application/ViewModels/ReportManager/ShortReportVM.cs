using DhubSolutions.Reports.Application.ViewModels.ReportManager.Base;
using System;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ShortReportVM : ShortBaseVM
    {
        public string Organization { get; set; }
        public string PeriodId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
