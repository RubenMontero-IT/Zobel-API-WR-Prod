using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class SPAMechanism : BaseEntity
    {
        public SPAMechanism() : base()
        {

        }
        public string Value { get; set; }
        public string Description { get; set; }

    }
}
