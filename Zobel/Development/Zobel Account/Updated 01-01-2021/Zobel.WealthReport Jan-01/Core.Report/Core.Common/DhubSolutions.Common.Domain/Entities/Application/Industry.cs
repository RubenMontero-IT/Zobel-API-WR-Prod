using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Industry : BaseEntity
    {
        public Industry() : base()
        {
        }
        public string IndustryValue { get; set; }
        public string Description { get; set; }

    }
}
