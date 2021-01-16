using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public ProductType() : base()
        {
            Products = new HashSet<Product>();
        }
        public string ProductTypeName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
