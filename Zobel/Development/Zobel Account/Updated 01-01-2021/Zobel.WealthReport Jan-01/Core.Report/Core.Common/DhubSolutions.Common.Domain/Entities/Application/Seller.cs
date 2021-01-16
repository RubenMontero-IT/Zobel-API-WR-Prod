using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Seller : BaseEntity
    {
        public Seller() : base()
        {
        }

        public string SellerValue { get; set; }
        public string Description { get; set; }

    }
}
