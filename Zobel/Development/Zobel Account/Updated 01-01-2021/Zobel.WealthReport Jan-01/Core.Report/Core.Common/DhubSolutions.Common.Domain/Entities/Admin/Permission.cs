using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class Permission : BaseEntity
    {
        public Permission() : base()
        {
            AppMaxPermission = new HashSet<AppMaxPermission>();
            DefaultRolePermission = new HashSet<DefaultRolePermission>();
            RoleAppPermission = new HashSet<RoleAppPermission>();
            RoleFilePermission = new HashSet<RoleFilePermission>();
            RoleProjectPermission = new HashSet<RoleProjectPermission>();
        }
        public string Description { get; set; }
        public string PermissionRelevance { get; set; }
        public string PermissionCode { get; set; }
        public string Style { get; set; }

        public ICollection<AppMaxPermission> AppMaxPermission { get; set; }
        public ICollection<DefaultRolePermission> DefaultRolePermission { get; set; }
        public ICollection<RoleAppPermission> RoleAppPermission { get; set; }
        public ICollection<RoleFilePermission> RoleFilePermission { get; set; }
        public ICollection<RoleProjectPermission> RoleProjectPermission { get; set; }

    }
}
