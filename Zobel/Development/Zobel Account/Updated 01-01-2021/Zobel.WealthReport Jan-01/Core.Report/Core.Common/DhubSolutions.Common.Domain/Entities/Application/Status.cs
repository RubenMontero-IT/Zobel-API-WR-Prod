using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Status : BaseEntity
    {
        public Status() : base()
        {

        }

        public string StatusValue { get; set; }
        public string Description { get; set; }


    }
}
