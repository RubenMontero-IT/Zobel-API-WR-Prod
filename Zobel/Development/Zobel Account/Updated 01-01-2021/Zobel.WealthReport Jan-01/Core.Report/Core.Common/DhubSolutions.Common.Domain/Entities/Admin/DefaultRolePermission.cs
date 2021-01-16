using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class DefaultRolePermission : BaseEntity
    {
        public DefaultRolePermission() : base()
        {

        }
        public string Rvid { get; set; }
        public string Orgid { get; set; }
        public string PermissionId { get; set; }
        public bool Active { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ModificationUser { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public OrganizationRole OrganizationRole { get; set; }
        public Permission Permission { get; set; }
    }
}
