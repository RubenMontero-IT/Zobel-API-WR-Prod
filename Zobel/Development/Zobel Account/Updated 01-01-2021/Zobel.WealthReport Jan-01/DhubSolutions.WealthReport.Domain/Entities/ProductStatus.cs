using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductStatus : BaseEntity
    {
        public ProductStatus() : base()
        {
        }
        public string ProductStatusName { get; set; }
    }
}
