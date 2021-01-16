using DhubSolutions.Common.Domain.Entities.Admin;
using System.Linq;

namespace DhubSolutions.Common.Domain.Entities.Base
{
    public static class AssignableResourceExtension
    {
        public static bool HasPermission(this IAssignableResource assignableResource, Permission permission, OrganizationRole organizationRole)
        {
            return assignableResource.PermissionId == permission.Id && assignableResource.OrganizationRoleId == organizationRole.Id;
        }

        public static bool HasPermission(this IAssignableResource assignableResource, Permission permission, Organization organization)
        {
            return organization.OrganizationsRoles.Any(orgRole => assignableResource.HasPermission(permission, orgRole));
        }
    }
}
