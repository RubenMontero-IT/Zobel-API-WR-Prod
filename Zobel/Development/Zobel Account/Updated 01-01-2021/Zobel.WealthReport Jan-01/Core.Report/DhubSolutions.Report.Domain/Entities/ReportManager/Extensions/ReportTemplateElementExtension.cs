using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Base;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions
{
    public static class ReportTemplateElementExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public static bool IsAccessible(this ReportTemplateElement reportTemplateElement, OrganizationRole organizationRole, Permission permission)
        {
            return reportTemplateElement.ReportTemplateElementPermissions.Any(
                reportTemplatePermission => reportTemplatePermission.HasPermission(permission, organizationRole));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static bool IsAccessible(this ReportTemplateElement reportTemplateElement, Organization organization, Permission permission)
        {
            return reportTemplateElement.ReportTemplateElementPermissions.Any(
                reportTemplateElementPermission => reportTemplateElementPermission.HasPermission(permission, organization));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplateElementPermission> GetPermissions(this ReportTemplateElement reportTemplateElement, OrganizationRole organizationRole)
        {
            return reportTemplateElement.ReportTemplateElementPermissions
                .Where(templatePermission => templatePermission.OrganizationRoleId == organizationRole.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplateElementPermission> GetPermissions(this ReportTemplateElement reportTemplateElement, Organization organization)
        {
            return reportTemplateElement.ReportTemplateElementPermissions
                .Where(templatePermission => templatePermission.OrganizationRole.OrganizationId == organization.Id);
        }
    }
}
