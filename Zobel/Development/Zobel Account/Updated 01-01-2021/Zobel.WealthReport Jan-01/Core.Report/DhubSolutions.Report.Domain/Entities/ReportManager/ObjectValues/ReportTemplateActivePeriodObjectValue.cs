using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    public class ReportTemplateActivePeriodObjectValue : IObjectValue
    {
        public ReportTemplateActivePeriodObjectValue(string period, IEnumerable<PeriodStatusObjectValue> periodStatuses)
        {
            Period = period;
            PeriodStatuses = periodStatuses.ToList();
        }

        public string Period { get; }

        public IEnumerable<PeriodStatusObjectValue> PeriodStatuses { get; }
    }
}
