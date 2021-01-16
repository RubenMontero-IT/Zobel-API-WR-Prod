using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Currency : BaseEntity
    {
        public Currency() : base()
        {

        }
        public string CurrencyValue { get; set; }
        public string Description { get; set; }



    }
}
