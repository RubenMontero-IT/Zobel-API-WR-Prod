using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class CommentedResource : BaseEntity
    {
        public CommentedResource() : base()
        {

        }
        public string ResourceId { get; set; }
        public string CommentId { get; set; }
        public string Description { get; set; }
#nullable enable
        public string? FeatureId { get; set; }

        public Feature Feature { get; set; }
        public Resource Resource { get; set; }

    }
}
