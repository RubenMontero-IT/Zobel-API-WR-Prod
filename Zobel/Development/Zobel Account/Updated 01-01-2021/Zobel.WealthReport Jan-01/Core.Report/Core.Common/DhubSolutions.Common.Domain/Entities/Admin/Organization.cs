using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class Organization : BaseEntity
    {
        public Organization() : base()
        {
            OrganizationsRoles = new HashSet<OrganizationRole>();
        }
        public int? LucanetId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDescription { get; set; }

#nullable enable
        public string? ResourceId { get; set; }
        public Resource Resource { get; set; }
        public ICollection<OrganizationRole> OrganizationsRoles { get; set; }
    }
}
