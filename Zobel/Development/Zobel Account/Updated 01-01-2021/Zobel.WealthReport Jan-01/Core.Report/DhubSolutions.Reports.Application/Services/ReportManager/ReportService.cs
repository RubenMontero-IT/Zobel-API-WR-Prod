using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.Reports.Domain.Services.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DhubSolutions.Reports.Application.Services.ReportManager
{
    public class ReportService : IReportService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationRoleRepository _organizationRoleRepository;
        private readonly IUserRoleOrgRepository _userRoleOrgRepository;
        private readonly IReportTemplateRepository _reportTemplateRepository;
        private readonly ITemplateSettingsRepository _templateSettingsRepository;
        private readonly IReportManagerService _reportManagerService;
        private readonly IReportRepository _reportRepository;
        private readonly IReportElementRepository _reportElementRepository;
        private readonly IPermissionRepository _permissionRepository;

        public IUnitOfWork UnitOfWork { get; }
        public ITypeAdapter TypeAdapter { get; }


        public ReportService(
            IUnitOfWork unitOfWork,
            ITypeAdapter typeAdapter,
            IReportManagerService reportManagerService,
            IReportRepository reportRepository,
            IReportElementRepository reportElementRepository,
            IReportTemplateRepository reportTemplateRepository,
            ITemplateSettingsRepository templateSettingsRepository,
            IOrganizationRepository organizationRepository,
            IOrganizationRoleRepository organizationRoleRepository,
            IUserRoleOrgRepository userRoleOrgRepository,
            IPermissionRepository permissionRepository)
        {
            UnitOfWork = unitOfWork;
            TypeAdapter = typeAdapter;
            _reportManagerService = reportManagerService;
            _reportRepository = reportRepository;
            _reportElementRepository = reportElementRepository;
            _reportTemplateRepository = reportTemplateRepository;
            _templateSettingsRepository = templateSettingsRepository;
            _organizationRepository = organizationRepository;
            _organizationRoleRepository = organizationRoleRepository;
            _userRoleOrgRepository = userRoleOrgRepository;
            _permissionRepository = permissionRepository;
        }


        public string CloneReport(string newReportName, Report report)
        {
            var reportCloneId = _reportRepository.CloneReport(report);
            report.Name = newReportName;

            if (UnitOfWork.SaveChanges() < 0)
                return null;

            return reportCloneId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRole"></param>
        /// <returns></returns>
        public string GetSettings(OrganizationRole organizationRole)
        {
            return _reportRepository.GetSettings(organizationRole);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Dto CreateReport<Dto>(ReportCreationDto dto) where Dto : class
        {
            ReportTemplate reportTemplate = TypeAdapter.Adapt<ReportTemplateDto, ReportTemplate>(dto.ReportTemplate);

            if (!_reportManagerService.CanProcessExpanderBlock(reportTemplate, dto.Organization, dto.Params, out JObject expandedValues, out string propertyId))
                return default;

            ReportTemplate postTemplate = _reportManagerService.GetPosTemplate(reportTemplate, dto.Params, expandedValues, propertyId);

            Report report = _reportManagerService.CreateReport(dto.ReportName, postTemplate, dto.User,
                                                      dto.Organization, dto.UserOrganizationRole, dto.Params);

            _reportRepository.Add(report);

            return TypeAdapter.Adapt<Dto>(report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void UpdateReportData(ReportDataUpdatedDto dto)
        {
            if (dto.DataBloks.Count() == 0)
                throw new InvalidOperationException("You must specify at least one data block name");

            Report report = _reportRepository.Get(r => r.Id == dto.ReportId,
                                                  asNoTracking: true,
                                                  r => r.Template,
                                                  r => r.ReportPermissions); ;
            if (report == null)
                throw new InvalidOperationException("Report not found");

            string organizationId = report.ReportPermissions.SingleOrDefault().OrganizationId;

            Organization organization = _organizationRepository.Get(org => org.Id == organizationId, asNoTracking: true);

            if (organization == null)
                throw new InvalidOperationException("Organization not found");

            var parameters = report.GetCreationOptions();

            bool canProcess = _reportManagerService.CanProcessExpanderBlock(report.Template, organization, parameters,
                                                                            out JObject expandedValues, out string propertyId);
            if (!canProcess)
                throw new InvalidOperationException("Can not process expander Object");

            ReportTemplate postTemplate = _reportManagerService.GetPosTemplate(report.Template, parameters, expandedValues, propertyId);

            _reportManagerService.FillReportData(report, postTemplate, parameters, dto.DataBloks.ToArray());

            report.LastModified = DateTime.Now;

            _reportRepository.Update(report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportUpdated"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public void UpdateReportContent(ReportUpdatedDto reportUpdated)
        {
            Report report = _reportRepository.Get(r => r.Id == reportUpdated.ReportId, asNoTracking: true);
            if (report == null)
                throw new InvalidOperationException($"{nameof(Report)} not found");

            string userId = reportUpdated.UserId;

            report.Name = reportUpdated.Name ?? report.Name;

            report.LastModified = DateTime.Now;

            _reportRepository.Update(report);

            if (reportUpdated.Removed.Count() != 0)
                RemoveReportElements(reportUpdated.Removed);

            if (reportUpdated.Added.Count() != 0)
                AddReportElements(reportUpdated.Added);

            if (reportUpdated.Updated.Count() != 0)
                UpdateReportElements(reportUpdated.Updated);


            /// <summary>
            /// 
            /// </summary>
            /// <param name="reportElementAddition"></param>
            /// <exception cref="InvalidOperationException"></exception>
            void AddReportElements(IEnumerable<ReportElementDto> reportElements)
            {
                foreach (ReportElementDto reportElementDto in reportElements)
                {
                    if (!reportElementDto.UpdateUpReferences(report.Id, reportElementDto.ContainerId))
                        throw new InvalidOperationException(nameof(reportElementDto.UpdateUpReferences));

                    foreach (var child in reportElementDto.GetAllContent())
                    {
                        //Add new reportElement
                        ReportElement reportElement = TypeAdapter.Adapt<ReportElementDto, ReportElement>(child);
                        reportElement.CreatedById = userId;
                        reportElement.LastModifiedById = userId;
                        reportElement.CreationDate = DateTime.Now;
                        reportElement.LastModified = DateTime.Now;

                        _reportElementRepository.Add(reportElement);
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="reportElementUpdate"></param>
            /// <exception cref="InvalidOperationException"></exception>
            void UpdateReportElements(IEnumerable<ReportElementDto> reportElements)
            {
                foreach (ReportElementDto reportElementDto in reportElements)
                {
                    if (!reportElementDto.UpdateUpReferences(report.Id, reportElementDto.ContainerId))
                        throw new InvalidOperationException(nameof(reportElementDto.UpdateUpReferences));

                    ReportElement reportElement = _reportElementRepository.Get(re => re.Id == reportElementDto.Id, asNoTracking: true);
                    if (reportElement == null)
                        throw new InvalidOperationException($"{nameof(reportElement)} with id {reportElementDto.Id} not found");

                    reportElement = TypeAdapter.Adapt(reportElementDto, reportElement);
                    reportElement.LastModifiedById = userId;
                    reportElement.LastModified = DateTime.Now;

                    _reportElementRepository.Update(reportElement);

                }
            }
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportElements"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveReportElements(IEnumerable<string> reportElementIds)
        {
            IEnumerable<ReportElement> reportElements = _reportElementRepository.GetAll(
                    reportElement => reportElementIds.Contains(reportElement.Id), asNoTracking: true);

            if (reportElements.Count() == 0)
                throw new InvalidOperationException("No reportElements found with the given ids");

            _reportElementRepository.RemoveRange(reportElements);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveReport(string reportId)
        {
            Report report = _reportRepository.Get(r => r.Id == reportId, asNoTracking: true);
            if (report == null)
                throw new InvalidOperationException("report not found");

            _reportRepository.Remove(report);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportIds"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveReportRange(params string[] reportIds)
        {
            IEnumerable<Report> reports = _reportRepository.GetAll(report => reportIds.Contains(report.Id), asNoTracking: true);
            if (reports.Count() == 0)
                throw new InvalidOperationException("No report found with the given ids");

            _reportRepository.RemoveRange(reports);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAllReport<Dto>(
            Expression<Func<Report, bool>> filter = null,
            bool asNoTracking = false,
            params Expression<Func<Report, object>>[] includes) where Dto : class
        {
            IEnumerable<Report> reports = _reportRepository.GetAll(filter, asNoTracking, includes);

            return TypeAdapter.Adapt<IEnumerable<Dto>>(reports);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="reportId"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public Dto GetReportById<Dto>(string reportId,
            bool asNoTracking = false,
            params Expression<Func<Report, object>>[] includes) where Dto : class
        {
            Report report = _reportRepository.Get(r => r.Id == reportId, asNoTracking, includes);

            return TypeAdapter.Adapt<Dto>(report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="detailReport"></param>
        /// <returns></returns>
        public Dto GetDetailReport<Dto>(DetailReportDto detailReport) where Dto : class
        {
            //Report report = _reportManagerService.PruneReportNodes(detailReport.Report, detailReport.OrganizationRole);
            Report report = detailReport.Report;

            OrganizationRole organizationRole = detailReport.OrganizationRole;

            if (!_reportManagerService.IsAccesible(report, organizationRole))
                return default;

            Permission readPermission = _permissionRepository.Get(p => p.PermissionCode == "read", asNoTracking: true);

            var reportContent = _reportManagerService.GetOnlyAccessibleModules(report.Content, organizationRole, readPermission);

            if (reportContent.Count() == 0)
                return default;

            report.Content = reportContent.ToList();
            JObject dataContent = _reportManagerService.GetOnlyAccessibleDataContents(report);
            report.Data = $"{ dataContent}";

            return TypeAdapter.Adapt<Dto>(report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="fullReport"></param>
        /// <returns></returns>
        public Dto GetFullReport<Dto>(FullReportDto fullReport) where Dto : class
        {
            _reportManagerService.SetMasterPermissions(fullReport.Report, fullReport.Organization);

            return TypeAdapter.Adapt<Dto>(fullReport.Report);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingsDto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddOrUpdateTemplateSettings(ReportTemplateSettingsDto settingsDto)
        {
            Report report = _reportRepository.Get(r => r.Id == settingsDto.ReportId,
                                                  asNoTracking: true,
                                                  r => r.Template,
                                                  r => r.ReportPermissions);
            if (report == null)
                throw new InvalidOperationException("Report not found");

            string organizationId = report.ReportPermissions.Single().OrganizationId;

            Organization organization = _organizationRepository.Get(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                throw new InvalidOperationException("Organization not found");

            if (report.Template == null)
                throw new InvalidOperationException("ReportTemplate not found");

            OrganizationRole organizationRole = _organizationRoleRepository.Get(orgRole => orgRole.Id == settingsDto.OrgRoleId, asNoTracking: true);
            if (organizationRole == null)
                throw new InvalidOperationException("OrganizationRole not found");

            UserRoleOrg userRoleOrg = _userRoleOrgRepository.Get(e => e.Orgid == organizationRole.OrganizationId &&
                                                                                e.Rvid == organizationRole.Rvid &&
                                                                                e.Userid == settingsDto.UserId,
                                                                asNoTracking: true);
            if (userRoleOrg == null)
                throw new InvalidOperationException("UserOrganizationRole not found");


            try
            {
                TemplateSettings templateSettings = _templateSettingsRepository.Get(/*organization,*/
                                          settings => settings.OrganizationId == organization.Id &&
                                                      settings.UserRoleOrgId == userRoleOrg.Id &&
                                                      settings.TemplateCode == report.Template.Code,
                                          asNoTracking: true);

                if (templateSettings == null)
                {
                    templateSettings = new TemplateSettings
                    {
                        OrganizationId = organization.Id,
                        TemplateCode = report.Template.Code,
                        UserRoleOrgId = userRoleOrg.Id,
                        Settings = $"{settingsDto.Settings}"
                    };

                    _templateSettingsRepository.Add(/*organization,*/ templateSettings);
                }
                else
                {
                    templateSettings.Settings = $"{settingsDto.Settings}";
                    _templateSettingsRepository.Update(/*organization,*/ templateSettings);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingsDto"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public JObject GetTemplateSettings(ReportTemplateSettingsDto settingsDto)
        {
            Report report = _reportRepository.Get(r => r.Id == settingsDto.ReportId,
                                                     asNoTracking: true,
                                                     r => r.Template,
                                                     r => r.ReportPermissions);
            if (report == null)
                throw new InvalidOperationException("Report not found");

            string organizationId = report.ReportPermissions.Single().OrganizationId;

            Organization organization = _organizationRepository.Get(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                throw new InvalidOperationException("Organization not found");

            if (report.Template == null)
                throw new InvalidOperationException("ReportTemplate not found");

            OrganizationRole organizationRole = _organizationRoleRepository.Get(orgRole => orgRole.Id == settingsDto.OrgRoleId, asNoTracking: true);
            if (organizationRole == null)
                throw new InvalidOperationException("OrganizationRole not found");

            UserRoleOrg userRoleOrg = _userRoleOrgRepository.Get(e => e.Orgid == organizationRole.OrganizationId &&
                                                                               e.Rvid == organizationRole.Rvid &&
                                                                               e.Userid == settingsDto.UserId,
                                                               asNoTracking: true);
            if (userRoleOrg == null)
                throw new InvalidOperationException("UserOrganizationRole not found");

            try
            {
                TemplateSettings templateSettings = _templateSettingsRepository
                                                           .Get(/*organization,*/
                                                                settings => settings.OrganizationId == organization.Id &&
                                                                            settings.UserRoleOrgId == userRoleOrg.Id &&
                                                                            settings.TemplateCode == report.Template.Code,
                                                             asNoTracking: true);

                if (templateSettings == null)
                    throw new InvalidOperationException("TemplateSettings not found");

                return JObject.Parse(templateSettings.Settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
