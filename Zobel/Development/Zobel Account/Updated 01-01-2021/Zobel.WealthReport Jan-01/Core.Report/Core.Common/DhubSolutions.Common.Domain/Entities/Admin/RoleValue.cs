using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    /// <summary>
    /// Entity for user roles (i.e. Secretary, Personal Chief, CEO)
    /// </summary>
    public class RoleValue : BaseEntity
    {
        public RoleValue() : base()
        {
            OrganizationRoles = new HashSet<OrganizationRole>();
            SharedComments = new HashSet<SharedComment>();
        }
        public string Value { get; set; }
        public string Description { get; set; }
#nullable enable
        public string? Rtid { get; set; }
        public bool IsDefault { get; set; }
        public string ContactEmail { get; set; }

        public RoleType Role { get; set; }
        public ICollection<OrganizationRole> OrganizationRoles { get; set; }
        public ICollection<SharedComment> SharedComments { get; set; }
    }
}
