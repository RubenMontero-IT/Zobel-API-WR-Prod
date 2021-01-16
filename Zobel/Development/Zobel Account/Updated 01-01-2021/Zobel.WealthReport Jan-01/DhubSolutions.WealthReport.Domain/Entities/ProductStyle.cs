using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductStyle : BaseEntity
    {
        public ProductStyle() : base()
        {
        }
        public string ProductStyleName { get; set; }
    }
}
