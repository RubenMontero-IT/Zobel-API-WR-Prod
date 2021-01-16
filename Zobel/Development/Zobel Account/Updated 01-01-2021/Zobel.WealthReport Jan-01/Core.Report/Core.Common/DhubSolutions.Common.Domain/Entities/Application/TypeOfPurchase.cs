using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class TypeOfPurchase : BaseEntity
    {
        public TypeOfPurchase() : base()
        {

        }
        public string Value { get; set; }
        public string Description { get; set; }


    }
}
