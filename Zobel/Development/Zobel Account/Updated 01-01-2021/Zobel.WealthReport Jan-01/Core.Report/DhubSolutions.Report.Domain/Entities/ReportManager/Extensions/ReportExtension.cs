using DhubSolutions.Common.Domain.Entities.Admin;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions
{
    public static class ReportExtension
    {
        public static bool IsAccessible(this Report report, OrganizationRole organizationRole, Permission permission)
        {
            return report.Template.IsAccessible(organizationRole, permission);
        }
    }
}
