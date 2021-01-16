using System.Linq;

namespace DhubSolutions.Common.Domain.Entities.Admin.Extensions
{
    public static class OrganizationRoleExtension
    {
        public static bool IsMemberOf(this OrganizationRole organizationRole, SystemGroup systemGroup)
        {
            return organizationRole.SystemGroupMemberShips.Any(s => s.SystemGroupId == systemGroup.Id);
        }

    }
}
