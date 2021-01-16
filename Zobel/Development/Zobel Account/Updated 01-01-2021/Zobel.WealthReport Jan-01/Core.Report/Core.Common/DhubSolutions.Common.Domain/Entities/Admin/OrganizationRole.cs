using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class OrganizationRole : BaseEntity
    {
        public OrganizationRole() : base()
        {
            AppSettingRoleVOrg = new HashSet<AppSettingRoleVOrg>();
            DefaultRolePermission = new HashSet<DefaultRolePermission>();
            RoleAppPermission = new HashSet<RoleAppPermission>();
            RoleFilePermission = new HashSet<RoleFilePermission>();
            RoleProjectPermission = new HashSet<RoleProjectPermission>();
            UserRoleOrg = new HashSet<UserRoleOrg>();
            SystemGroupMemberShips = new HashSet<SystemGroupMemberShip>();
        }

        public string Rvid { get; set; }
        public string OrganizationId { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
        public Organization Organization { get; set; }
        public RoleValue RoleValue { get; set; }
        public ICollection<DefaultRolePermission> DefaultRolePermission { get; set; }
        public ICollection<UserRoleOrg> UserRoleOrg { get; set; }
        public ICollection<AppSettingRoleVOrg> AppSettingRoleVOrg { get; set; }
        public ICollection<RoleAppPermission> RoleAppPermission { get; set; }
        public ICollection<RoleFilePermission> RoleFilePermission { get; set; }
        public ICollection<RoleProjectPermission> RoleProjectPermission { get; set; }
        public ICollection<SystemGroupMemberShip> SystemGroupMemberShips { get; set; }

    }
}
