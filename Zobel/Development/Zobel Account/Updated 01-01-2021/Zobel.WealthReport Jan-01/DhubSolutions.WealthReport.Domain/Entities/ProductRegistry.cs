using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductRegistry : BaseEntity
    {
        public ProductRegistry() : base()
        {
            ProductExtendedRegistries = new HashSet<ProductExtendedRegistry>();
        }

        public string ProductID { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public Product Product { get; set; }
        public ICollection<ProductExtendedRegistry> ProductExtendedRegistries { get; set; }
    }
}
