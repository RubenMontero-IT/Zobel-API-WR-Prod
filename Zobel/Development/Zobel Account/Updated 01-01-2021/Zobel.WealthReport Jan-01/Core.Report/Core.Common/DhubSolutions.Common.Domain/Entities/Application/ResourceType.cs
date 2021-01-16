using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class ResourceType : BaseEntity
    {
        public ResourceType() : base()
        {
            Features = new HashSet<Feature>();
            Resources = new HashSet<Resource>();
        }
        public string ResourceTypeName { get; set; }
        public string Description { get; set; }

        public ICollection<Feature> Features { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
