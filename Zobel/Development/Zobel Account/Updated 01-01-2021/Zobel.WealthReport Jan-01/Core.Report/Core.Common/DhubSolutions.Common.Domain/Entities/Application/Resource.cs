using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Resource : BaseEntity
    {
        public Resource() : base()
        {
            CommentedResources = new HashSet<CommentedResource>();
            Organizations = new HashSet<Organization>();
            Users = new HashSet<User>();
        }
        public string Description { get; set; }
#nullable enable
        public string? ResourceType { get; set; }

        public ResourceType ResourceTypeNavigation { get; set; }
        public ICollection<CommentedResource> CommentedResources { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
