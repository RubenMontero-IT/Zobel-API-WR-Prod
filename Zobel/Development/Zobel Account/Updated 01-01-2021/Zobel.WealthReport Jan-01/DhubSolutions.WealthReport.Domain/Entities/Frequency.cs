using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Frequency : BaseEntity
    {
        public Frequency() : base()
        {
            ProductFrequencies = new HashSet<ProductFrequency>();
        }

        public string FrequencyName { get; set; }
        public ICollection<ProductFrequency> ProductFrequencies { get; set; }
    }
}
