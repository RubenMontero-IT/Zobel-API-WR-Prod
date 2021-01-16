using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Region() : base()
        {
            Products = new HashSet<Product>();
        }

        public string RegionName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
