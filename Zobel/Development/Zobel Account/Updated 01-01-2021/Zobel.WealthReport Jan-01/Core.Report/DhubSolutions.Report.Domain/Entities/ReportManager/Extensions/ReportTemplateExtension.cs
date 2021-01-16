using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Base;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Extensions
{
    public static class ReportTemplateExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplate"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static bool IsAccessible(this ReportTemplate reportTemplate, Organization organization)
        {
            return reportTemplate.ReportTemplatePermissions.Any(reportTemplatePermission =>
                                           reportTemplatePermission.OrganizationId == organization.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public static bool IsAccessible(this ReportTemplate reportTemplate, OrganizationRole organizationRole, Permission permission)
        {
            return reportTemplate.ReportTemplatePermissions.Any(reportTemplatePermission =>
                                          reportTemplatePermission.HasPermission(permission, organizationRole));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static bool IsAccessible(this ReportTemplate reportTemplate, Organization organization, Permission permission)
        {
            return reportTemplate.ReportTemplatePermissions.Any(reportTemplatePermission =>
                                          reportTemplatePermission.HasPermission(permission, organization));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static bool IsAvailable(this ReportTemplate template, Organization organization)
        {
            return template.ActivePeriods.Any(p => p.OrganizationId == organization.Id && p.IsActive == true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplateActivePeriod> GetActivePeriods(this ReportTemplate reportTemplate, Organization organization)
        {
            var dummyPeriods = GetDummyActivePeriods().ToDictionary(key => key.Period);

            var activePeriods = reportTemplate.ActivePeriods.Where(activePeriod => activePeriod.OrganizationId == organization.Id);

            foreach (var activePeriod in activePeriods)
            {
                dummyPeriods[activePeriod.Period] = activePeriod;
            }

            return dummyPeriods.Values;


            IEnumerable<ReportTemplateActivePeriod> GetDummyActivePeriods()
            {
                var date = DateTime.Today;

                date = date.AddMonths(-1);
                yield return new ReportTemplateActivePeriod
                {
                    Period = $"{date:yyyy-MM}",
                    IsActive = true,
                    ReportTemplateId = reportTemplate.Id,
                    OrganizationId = organization.Id
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplatePermission> GetPermissions(this ReportTemplate reportTemplate, OrganizationRole organizationRole)
        {
            return reportTemplate.ReportTemplatePermissions
                .Where(templatePermission => templatePermission.OrganizationRoleId == organizationRole.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplatePermission> GetPermissions(this ReportTemplate reportTemplate, Organization organization)
        {
            return reportTemplate.ReportTemplatePermissions
                .Where(templatePermission => templatePermission.OrganizationRole.OrganizationId == organization.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplate"></param>
        /// <param name="organizations"></param>
        /// <returns></returns>
        public static IEnumerable<ReportTemplateActivePeriodObjectValue> GetAllActivePeriods(this ReportTemplate reportTemplate, IEnumerable<Organization> organizations)
        {
            var periods = organizations.GetLastYear()
                .ToDictionary(templateActivePeriod =>
                                    templateActivePeriod.Period,

                              templateActivePeriod =>
                                    templateActivePeriod.PeriodStatuses
                                        .ToDictionary(periodStatus => periodStatus.Organization));

            var reportTemplateActivePeriods = reportTemplate.GetActivePeriods();

            foreach (var activePeriod in reportTemplateActivePeriods)
            {
                foreach (var periodStatus in activePeriod.PeriodStatuses)
                {
                    periods[activePeriod.Period][periodStatus.Organization] = periodStatus;
                }
            }

            return periods.Select(keyValuePair =>
                        new ReportTemplateActivePeriodObjectValue(
                                                   period: keyValuePair.Key,
                                                   periodStatuses: keyValuePair.Value.Values));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplate"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static IEnumerable<CreationPeriodObjectValue> GetAllCreationPeriods(this ReportTemplate reportTemplate, Organization organization)
        {
            var periods = new List<Organization> { organization }.GetLastYear()
                          .ToDictionary(key => key.Period);

            var reportTemplateActivePeriods = reportTemplate.GetActivePeriods()
                .Where(activePeriod => activePeriod.PeriodStatuses
                        .Any(periodStatus => periodStatus.Organization == organization.Id));

            foreach (var activePeriod in reportTemplateActivePeriods)
            {
                periods[activePeriod.Period] = activePeriod;
            }

            return periods.Values
                .Where(p =>
                    p.PeriodStatuses.Any(periodStatus =>
                                periodStatus.IsActivePeriod == true && periodStatus.Organization == organization.Id))
                .Select(p => new CreationPeriodObjectValue(p.Period));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        public static ReportTemplatePermissionObjectValue GetTemplatePermissions(this ReportTemplate template, Organization organization, Permission permission)
        {

            bool isAccessible = template.IsAccessible(organization, permission);
            if (!isAccessible)
                return default;

            var templatePermissions = template
                .GetPermissions(organization)
                .Select(p => new PermissionObjectValue(
                    organizationRoleId: p.OrganizationRoleId,
                    typePermission: p.Permission.PermissionCode));

            var elementsPermissions = template.GetAllContent()
                .Where(templateElement => templateElement.IsAccessible(organization, permission))
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
        public static ReportTemplatePermissionObjectValue GetTemplatePermissions(this ReportTemplate template, OrganizationRole organizationRole, Permission permission)
        {
            // get the full template from repo
            // clean the template according to the permission
            // return the template

            //filter the templatePermissions

            bool isAccessible = template.IsAccessible(organizationRole, permission);
            if (!isAccessible)
                return null;

            var templatePermissions = template.GetPermissions(organizationRole)
                .Select(p => new PermissionObjectValue(
                                  organizationRoleId: p.OrganizationRoleId,
                                  typePermission: p.Permission.PermissionCode));

            var elementsPermissions = template.GetAllContent()
                .Where(templateElement => templateElement.IsAccessible(organizationRole, permission))
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


        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizations"></param>
        /// <returns></returns>
        private static IEnumerable<ReportTemplateActivePeriodObjectValue> GetLastYear(this IEnumerable<Organization> organizations)
        {
            var date = DateTime.Today;

            //t-one is always true by default
            date = date.AddMonths(-1);
            yield return new ReportTemplateActivePeriodObjectValue(
                period: $"{date:yyyy-MM}",
                periodStatuses: organizations.Select(
                    organization => new PeriodStatusObjectValue(organization.Id, isActivePeriod: true)));


            foreach (var _ in Enumerable.Range(0, 11))
            {
                date = date.AddMonths(-1);
                yield return new ReportTemplateActivePeriodObjectValue(
                 period: $"{date:yyyy-MM}",
                periodStatuses: organizations.Select(
                    organization => new PeriodStatusObjectValue(organization.Id, isActivePeriod: false)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private static IEnumerable<ReportTemplateActivePeriodObjectValue> GetActivePeriods(this ReportTemplate template)
        {
            return template.ActivePeriods.GroupBy(activePeriod => activePeriod.Period,

                                                     activePeriod => new PeriodStatusObjectValue(
                                                         organization: activePeriod.OrganizationId,
                                                         isActivePeriod: activePeriod.IsActive),

                                                     (period, periodStatuses) => new ReportTemplateActivePeriodObjectValue(
                                                         period: period,
                                                         periodStatuses: periodStatuses));
        }

        #endregion
    }
}
