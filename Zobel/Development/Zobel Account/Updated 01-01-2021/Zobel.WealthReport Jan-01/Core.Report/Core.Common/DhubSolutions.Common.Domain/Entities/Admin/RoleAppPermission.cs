using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class RoleAppPermission : BaseEntity
    {
        public RoleAppPermission() : base()
        {

        }
        public string Rvid { get; set; }
        public string Orgid { get; set; }
        public string Appid { get; set; }
        public string PermissionId { get; set; }
        public bool WithGrant { get; set; }
        public bool Denied { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public Apps App { get; set; }
        public OrganizationRole OrganizationRole { get; set; }
        public Permission PermissionUser { get; set; }
    }
}
