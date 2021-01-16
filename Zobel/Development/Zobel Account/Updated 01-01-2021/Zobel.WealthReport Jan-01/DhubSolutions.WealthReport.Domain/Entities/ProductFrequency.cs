using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductFrequency : BaseEntity
    {
        public ProductFrequency() : base()
        {
        }

        public string FrequencyID { get; set; }
        public string ProductID { get; set; }

        public Frequency Frequency { get; set; }
        public Product Product { get; set; }
    }
}
