using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class ProcessType : BaseEntity
    {
        public ProcessType()
        {
        }
        public string ProcessTypeValue { get; set; }
        public string Description { get; set; }

    }
}
