using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues;

namespace DhubSolutions.Reports.Domain.Services.Base
{
    public interface IReportTemplateManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        ReportTemplatePermissionObjectValue GetTemplatePermissions(ReportTemplate template, OrganizationRole organizationRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        ReportTemplatePermissionObjectValue GetTemplatePermissions(ReportTemplate template, Organization organization);


    }
}