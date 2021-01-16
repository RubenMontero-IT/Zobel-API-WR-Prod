using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    public class CreationPeriodObjectValue : IObjectValue
    {
        public CreationPeriodObjectValue(string periodId)
        {
            PeriodId = periodId;
        }
        public string PeriodId { get; }
    }
}
