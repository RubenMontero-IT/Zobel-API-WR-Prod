using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Feature : BaseEntity
    {
        public Feature() : base()
        {
            CommentedResources = new HashSet<CommentedResource>();
        }
        public string ResourceTypeId { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }

        public ResourceType ResourceType { get; set; }
        public ICollection<CommentedResource> CommentedResources { get; set; }
    }
}
