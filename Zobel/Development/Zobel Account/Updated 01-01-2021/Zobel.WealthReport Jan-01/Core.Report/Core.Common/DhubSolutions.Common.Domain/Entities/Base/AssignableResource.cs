using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Base
{
    public class AssignableResource : BaseEntity, IAssignableResource
    {
        public AssignableResource() : base()
        { }
        public string OrganizationRoleId { get; set; }
        public string PermissionId { get; set; }
        public OrganizationRole OrganizationRole { get; set; }
        public Permission Permission { get; set; }




    }
}
