using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Style : BaseEntity
    {
        public Style() : base()
        {
            Products = new HashSet<Product>();
        }

        public string StyleName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
