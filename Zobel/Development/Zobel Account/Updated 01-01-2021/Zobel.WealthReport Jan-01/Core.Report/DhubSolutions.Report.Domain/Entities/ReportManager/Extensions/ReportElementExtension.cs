using DhubSolutions.Common.Domain.Entities.Admin;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions
{
    public static class ReportElementExtension
    {
        public static bool IsAccessible(this ReportElement reportElement, OrganizationRole organizationRole, Permission permission)
        {
            return reportElement.ReportTemplateElement.IsAccessible(organizationRole, permission);
        }
    }
}
