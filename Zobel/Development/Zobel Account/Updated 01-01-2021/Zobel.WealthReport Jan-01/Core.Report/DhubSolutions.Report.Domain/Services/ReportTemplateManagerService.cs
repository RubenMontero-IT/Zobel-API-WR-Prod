using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions;
using DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues;
using DhubSolutions.Reports.Domain.Services.Base;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportTemplateManagerService : IReportTemplateManagerService
    {
        private readonly IPermissionRepository _permissionRepository;


        public ReportTemplateManagerService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        public ReportTemplatePermissionObjectValue GetTemplatePermissions(ReportTemplate template, Organization organization)
        {
            Permission readPermission = _permissionRepository.Get(p => p.PermissionCode == "read");

            bool isAccessible = template.IsAccessible(organization, readPermission);
            if (!isAccessible)
                return null;

            var templatePermissions = template
                .GetPermissions(organization)
                .Select(p => new PermissionObjectValue(
                    organizationRoleId: p.OrganizationRoleId,
                    typePermission: p.Permission.PermissionCode));

            var elementsPermissions = template.GetAllContent()
                .Where(templateElement => templateElement.IsAccessible(organization, readPermission))
                .Select(templateElement => GetElementPermission(templateElement, organization));

            return new ReportTemplatePermissionObjectValue(
               permissions: templatePermissions,
               reportTemplateElementPermissions: elementsPermissions);


            ReportTemplateElementPermissionObjectValue GetElementPermission(ReportTemplateElement templateElement, Organization org)
            {
                var templateElementPermissions = templateElement
                    .GetPermissions(org);

                return new ReportTemplateElementPermissionObjectValue(
                    elementId: templateElement.Id,
                    permissions: templateElementPermissions
                                    .Select(p => new PermissionObjectValue(
                                               organizationRoleId: p.OrganizationRoleId,
                                               typePermission: p.Permission.PermissionCode)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public ReportTemplatePermissionObjectValue GetTemplatePermissions(ReportTemplate template, OrganizationRole organizationRole)
        {
            // get the full template from repo
            // clean the template according to the permission
            // return the template

            Permission readPermission = _permissionRepository.Get(p => p.PermissionCode == "read");

            //filter the templatePermissions

            bool isAccessible = template.IsAccessible(organizationRole, readPermission);
            if (!isAccessible)
                return null;

            var templatePermissions = template.GetPermissions(organizationRole)
                .Select(p => new PermissionObjectValue(
                                  organizationRoleId: p.OrganizationRoleId,
                                  typePermission: p.Permission.PermissionCode));

            var elementsPermissions = template.GetAllContent()
                .Where(templateElement => templateElement.IsAccessible(organizationRole, readPermission))
                .Select(templateElement => GetRepotTemplateElementPermission(templateElement, organizationRole));

            return new ReportTemplatePermissionObjectValue(
                permissions: templatePermissions,
                reportTemplateElementPermissions: elementsPermissions);

            ReportTemplateElementPermissionObjectValue GetRepotTemplateElementPermission(ReportTemplateElement templateElement, OrganizationRole orgRole)
            {
                var templateElementPermissions = templateElement
                    .GetPermissions(organizationRole);

                return new ReportTemplateElementPermissionObjectValue(
                    elementId: templateElement.Id,
                    permissions: templateElementPermissions
                                    .Select(p => new PermissionObjectValue(
                                                organizationRoleId: p.OrganizationRoleId,
                                                typePermission: p.Permission.PermissionCode)));
            }
        }



    }
}
