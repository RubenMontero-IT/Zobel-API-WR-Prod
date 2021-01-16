using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class MetricsCategories : BaseEntity
    {
        public MetricsCategories() : base()
        {
        }

        public string MetricCode { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Period { get; set; }
        public string Plevel { get; set; }

    }
}
