using Domain.Entities.Abstractions;
using Domain.Entities.ApplicationEntities;
using System.Collections.Generic;

namespace Domain.Entities.AdminEntities
{
    public class Organization : BaseEntity
    {
        public Organization()
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
