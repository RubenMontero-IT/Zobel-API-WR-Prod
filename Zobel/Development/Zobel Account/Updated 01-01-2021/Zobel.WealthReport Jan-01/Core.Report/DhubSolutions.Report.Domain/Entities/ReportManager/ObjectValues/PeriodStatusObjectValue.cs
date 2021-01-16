using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    public class PeriodStatusObjectValue : IObjectValue
    {
        public PeriodStatusObjectValue(string organization, bool isActivePeriod = false)
        {
            Organization = organization;
            IsActivePeriod = isActivePeriod;
        }
        public string Organization { get; }

        public bool IsActivePeriod { get; }

    }
}
