using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class SaleReason : BaseEntity
    {
        public SaleReason() : base()
        {
        }
        public string SaleReasonValue { get; set; }
        public string Description { get; set; }

    }
}
