using DhubSolutions.Common.Domain.Entities.Admin;

namespace DhubSolutions.Common.Domain.Entities.Base
{
    public interface IAssignableResource
    {
        string OrganizationRoleId { get; set; }
        string PermissionId { get; set; }
        OrganizationRole OrganizationRole { get; set; }
        Permission Permission { get; set; }

    }

}
