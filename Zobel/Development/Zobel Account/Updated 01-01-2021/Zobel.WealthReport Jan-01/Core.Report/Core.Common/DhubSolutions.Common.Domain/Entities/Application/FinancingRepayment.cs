using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class FinancingRepayment : BaseEntity
    {
        public FinancingRepayment() : base()
        {

        }

        public string Value { get; set; }
        public string Description { get; set; }

    }
}
