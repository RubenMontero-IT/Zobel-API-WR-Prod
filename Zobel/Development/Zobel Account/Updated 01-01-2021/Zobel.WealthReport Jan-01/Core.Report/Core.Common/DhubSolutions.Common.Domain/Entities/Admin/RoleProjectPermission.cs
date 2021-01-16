using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class RoleProjectPermission : BaseEntity
    {

        public RoleProjectPermission() : base()
        {

        }
        public string Rvid { get; set; }
        public string Orgid { get; set; }

        public string ProjectId { get; set; }
        public string PermissionId { get; set; }
        public bool WithGrant { get; set; }
        public bool Denied { get; set; }
        public DateTime? ExpirationDateTime { get; set; }

        public OrganizationRole OrganizationRole { get; set; }
        public Permission Permissions { get; set; }


    }
}
