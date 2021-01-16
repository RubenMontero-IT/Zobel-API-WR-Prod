using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Domain.Services.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReportManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        IInstructionProcessorFactoryProxy InstructionProcessorFactoryProxy { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="template"></param>
        /// <param name="user"></param>
        /// <param name="organization"></param>
        /// <param name="userOrganizationRole"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Report CreateReport(
            string name,
            ReportTemplate template,
            User user,
            Organization organization,
            OrganizationRole userOrganizationRole,
            Dictionary<string, dynamic> parameters);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="template"></param>
        /// <param name="parameters"></param>
        /// <param name="datablocks"></param>
        void FillReportData(Report report, ReportTemplate template, Dictionary<string, dynamic> parameters, params string[] datablocks);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="organization"></param>
        void SetMasterPermissions(Report report, Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplate"></param>
        /// <param name="organization"></param>
        /// <param name="parameters"></param>
        /// <param name="expandedValues"></param>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        bool CanProcessExpanderBlock(ReportTemplate reportTemplate, Organization organization, Dictionary<string, dynamic> parameters, out JObject expandedValues, out string propertyId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportTemplate"></param>
        /// <param name="parameters"></param>
        /// <param name="expandedValues"></param>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        ReportTemplate GetPosTemplate(ReportTemplate reportTemplate, Dictionary<string, dynamic> parameters, JObject expandedValues, string propertyId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="organizationRole"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        IEnumerable<ReportElement> GetOnlyAccessibleModules(IEnumerable<ReportElement> modules, OrganizationRole organizationRole, Permission permission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="organizationRole"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        IEnumerable<ReportElement> GetOnlyAccessibleSections(IEnumerable<ReportElement> sections, OrganizationRole organizationRole, Permission permission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportElements"></param>
        /// <param name="organizationRole"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        IEnumerable<ReportElement> GetOnlyAccessiblePages(IEnumerable<ReportElement> reportElements, OrganizationRole organizationRole, Permission permission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        JObject GetOnlyAccessibleDataContents(Report report);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        bool IsAccesible(Report report, OrganizationRole organizationRole);
    }
}
