using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Entities.Admin.Extensions;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Api.Controllers.Base;
using DhubSolutions.WealthReport.Api.Errors;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Api.Controllers
{
    [Route("api/[controller]/{orgRoleId}/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WealthReportController : BaseController
    {
        private readonly ITypeAdapter _typeAdapter;
        private readonly IOrganizationManagerService _organizationManagerService;
        private readonly IReportService _reportService;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationRoleService _organizationRoleService;
        private readonly ISystemGroupService _systemGroupService;
        private readonly IReportTemplateService _reportTemplateService;
        private readonly IPermissionService _permissionService;
        private readonly IStatementCategoryService _statementCategoryService;
        private readonly ISignableStatementService _signableStatementService;
        private readonly IWealthReportDataRepository _wealthReportDataRepository;

        public WealthReportController(
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            ITypeAdapter typeAdapter,
            IOrganizationManagerService organizationManagerService,
            IReportService reportService,
            IReportTemplateService reportTemplateService,
            IOrganizationService organizationService,
            IOrganizationRoleService organizationRoleService,
            ISystemGroupService systemGroupService,
            IPermissionService permissionService,
            IStatementCategoryService statementCategoryService,
            ISignableStatementService signableStatementService,
            IWealthReportDataRepository wealthReportDataRepository) : base(unitOfWork, userManager)
        {
            _typeAdapter = typeAdapter;
            _organizationManagerService = organizationManagerService;
            _reportService = reportService;
            _reportTemplateService = reportTemplateService;
            _organizationService = organizationService;
            _organizationRoleService = organizationRoleService;
            _systemGroupService = systemGroupService;
            _permissionService = permissionService;
            _statementCategoryService = statementCategoryService;
            _signableStatementService = signableStatementService;
            _wealthReportDataRepository = wealthReportDataRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reports")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ShortReportVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> CreateReport([FromRoute] string orgRoleId, [FromBody] ReportCreationVM model)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService
                                                .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                                .Get<Organization>(org => org.Id == model.OrganizationId);
            if (organization == null)
                return NotFound($"Organization not found");

            ReportTemplateDto reportTemplate = _reportTemplateService.GetTemplateById<ReportTemplateDto>
                            (model.TemplateId, asNotracking: true);

            if (reportTemplate == null)
                return NotFound("ReportTemplate not found");

            User user = await GetCurrentUser();
            if (user is null)
                return NotFound("Current user not found");

            var reportCreationDto = new ReportCreationDto()
            {
                ReportName = model.ReportName,
                ReportTemplate = reportTemplate,
                Organization = organization,
                UserOrganizationRole = organizationRole,
                User = user,
                Params = model.Params
            };

            ShortReportVM report = _reportService.CreateReport<ShortReportVM>(reportCreationDto);
            report.CreatedBy = user.UserName;

            if (_unitOfWork.SaveChanges() < 0)
                return BadRequest("Error when trying to insert a report");

            return CreatedAtAction(nameof(CreateReport), report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <param name="reportUpdateData"></param>
        /// <returns></returns>
        [HttpPut("reports/{reportId}/data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public async Task<IActionResult> UpdateReportData(
            [FromRoute] string orgRoleId,
            [FromRoute] string reportId,
            [FromBody] ReportUpdateDataVM reportUpdateData)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("organizationRoleId Parameter cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("reportId Parameter cant be null"));

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            OrganizationRole organizationRole = _organizationRoleService
                                                .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                                                                       asNoTracking: true,
                                                                       orgRole => orgRole.Organization,
                                                                       orgRole => orgRole.SystemGroupMemberShips);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            User user = await GetCurrentUser();
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("User not found"));

            try
            {
                ReportDataUpdatedDto dto = new ReportDataUpdatedDto
                {
                    ReportId = reportId,
                    DataBloks = reportUpdateData.DataBlocks
                };

                _reportService.UpdateReportData(dto);

                if (_unitOfWork.SaveChanges() < 0)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new BadRequestError("Error when trying to update a report data"));

                Report report = _reportService.GetReportById<Report>(reportId, asNoTracking: true,
                                      r => r.CreatedBy,
                                      r => r.Content,
                                      r => r.ReportPermissions,
                                      r => r.Template.ActivePeriods);

                if (report is null)
                    return NotFound("Report not found");

                SystemGroup systemGroup = _systemGroupService
                                      .Get<SystemGroup>(s => s.Code == "wr_master",
                                              asNoTracking: true);
                if (systemGroup == null)
                    return BadRequest("SystemGroup not found");

                ReportVM reportVM = _typeAdapter.Adapt<Report, ReportVM>(report);

                //if (!organizationRole.IsMemberOf(systemGroup))
                //{
                //    DetailReportDto detailReport = new DetailReportDto
                //    {
                //        OrganizationRole = organizationRole,
                //        Report = report
                //    };
                //    reportVM = _reportService.GetDetailReport<ReportVM>(detailReport);
                //    if (reportVM is null)
                //        return BadRequest("The current organizationRole is not authorized to access this report");

                //}
                return StatusCode(StatusCodes.Status200OK, $"{reportVM.Data}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("reports/{reportId}/content")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> UpdateReportContent([FromRoute] string orgRoleId, [FromRoute] string reportId, [FromBody] ReportUpdateVM model)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("reportId Parameter cant be null"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService
                                                .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError($"OrganizationRole not found"));

            User user = await GetCurrentUser();

            ReportUpdatedDto reportUpdated = _typeAdapter.Adapt<ReportUpdateVM, ReportUpdatedDto>(model);
            reportUpdated.ReportId = reportId;
            reportUpdated.UserId = user.Id;

            try
            {
                _reportService.UpdateReportContent(reportUpdated);

                if (_unitOfWork.SaveChanges() < 0)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new BadRequestError("Error when trying to update a report content"));

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NullReferenceException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError(e.Message));
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <returns></returns>
        [HttpGet("reports")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ShortReportVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllReport([FromRoute] string orgRoleId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                            .Get<OrganizationRole>(org => org.Id == orgRoleId,
                                asNoTracking: true,
                                includes: org => org.SystemGroupMemberShips);

            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            IEnumerable<Report> reports = _reportService.GetAllReport<Report>(null,
                                                asNoTracking: true,
                                                report => report.ReportPermissions,
                                                report => report.CreatedBy,
                                                report => report.Template.ReportTemplatePermissions);

            SystemGroup systemGroup = _systemGroupService
                                                .Get<SystemGroup>(s => s.Code == "wr_master",
                                                            asNoTracking: true);
            if (systemGroup == null)
                return BadRequest("SystemGroup not found");

            if (!organizationRole.IsMemberOf(systemGroup))
            {
                //Permission permission = _permissionService
                //                                .Get<Permission>(p => p.PermissionCode == "read",
                //                                            asNoTracking: true);

                reports = reports.Where(report => report.ReportPermissions
                                          .Any(reportPermission =>
                                                  reportPermission.OrganizationId == organizationRole.OrganizationId));
                //.Where(report => report.IsAccessible(organizationRole, permission));
            }
            return Ok(_typeAdapter.Adapt<IEnumerable<ShortReportVM>>(reports));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpGet("reports/{reportId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetReportDetails([FromRoute] string orgRoleId, [FromRoute] string reportId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return BadRequest("reportId Parameter can't be null");

            OrganizationRole organizationRole = _organizationRoleService
                                       .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                                           asNoTracking: true,
                                           orgRole => orgRole.Organization,
                                           orgRole => orgRole.SystemGroupMemberShips);

            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Report report = _reportService.GetReportById<Report>(reportId, asNoTracking: true,
                                     r => r.CreatedBy,
                                     r => r.Content,
                                     r => r.ReportPermissions,
                                     r => r.Template.ActivePeriods);

            if (report is null)
                return NotFound("Report not found");

            return Ok(_typeAdapter.Adapt<Report, ReportVM>(report));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpDelete("reports/{reportId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public IActionResult RemoveReport([FromRoute] string orgRoleId, [FromRoute] string reportId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return BadRequest("reportId Parameter can't be null");

            //SystemGroup systemGroup = _systemGroupService
            //                           .Get<SystemGroup>(s => s.Code == "wr_master",
            //                                   asNoTracking: true);
            //if (systemGroup == null)
            //    return BadRequest("SystemGroup not found");

            OrganizationRole organizationRole = _organizationRoleService
                                       .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                                           asNoTracking: true,
                                           orgRole => orgRole.SystemGroupMemberShips);

            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            try
            {
                //if (!organizationRole.IsMemberOf(systemGroup))
                //    return StatusCode(StatusCodes.Status400BadRequest,
                //            new BadRequestError("The current organization Role does not have permissions to perform this action"));

                _reportService.RemoveReport(reportId);
                _reportService.UnitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status200OK);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError(ex.Message));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportIds"></param>
        /// <returns></returns>
        [HttpDelete("reports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public IActionResult RemoveReport([FromRoute] string orgRoleId, [FromBody] string[] reportIds)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            //SystemGroup systemGroup = _systemGroupService
            //                           .Get<SystemGroup>(s => s.Code == "wr_master",
            //                                   asNoTracking: true);
            //if (systemGroup == null)
            //    return BadRequest("SystemGroup not found");

            OrganizationRole organizationRole = _organizationRoleService
                                       .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                                           asNoTracking: true,
                                           orgRole => orgRole.SystemGroupMemberShips);

            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            try
            {
                //if (!organizationRole.IsMemberOf(systemGroup))
                //    return StatusCode(StatusCodes.Status400BadRequest,
                //            new BadRequestError("The current organization Role does not have permissions to perform this action"));

                _reportService.RemoveReportRange(reportIds);
                _reportService.UnitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status200OK);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError(ex.Message));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <returns></returns>
        [HttpGet("organizations")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganizationVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllOrganizations([FromRoute] string orgRoleId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            SystemGroup systemGroup = _systemGroupService
                                       .Get<SystemGroup>(s => s.Code == "wr_master",
                                               asNoTracking: true);
            if (systemGroup == null)
                return BadRequest("SystemGroup not found");

            OrganizationRole organizationRole = _organizationRoleService
                                       .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                                           asNoTracking: true,
                                           orgRole => orgRole.SystemGroupMemberShips);

            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            IEnumerable<OrganizationVM> organizations;
            if (organizationRole.IsMemberOf(systemGroup))
            {
                IEnumerable<string> organizationIDs = _organizationManagerService.GetAllConnectionByOrganizations()
                                                                                 .Select(conn => conn.Id);

                organizations = _organizationService.GetAll<OrganizationVM>(org => organizationIDs.Contains(org.Id), asNoTracking: true);
            }
            else
            {
                Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationRole.OrganizationId, asNoTracking: true);
                if (organization == null)
                    return BadRequest("Organization not founds");

                ConnectionByOrganization connectionByOrganization = _organizationManagerService
                                                                            .GetConnectionByOrganization(organization);
                if (connectionByOrganization == null)
                    return BadRequest("ConnectionByOrganization not founds");

                organizations = _organizationService.GetAll<OrganizationVM>(org => org.Id == connectionByOrganization.Id, asNoTracking: true);
            }

            if (organizations.Count() == 0)
                return BadRequest("Organizations not founds");

            return Ok(organizations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/templates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShortReportTemplateVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public IActionResult GetAllTemplates([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("organizationRoleId Parameter cant be null"));

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("organizationRoleId Parameter cant be null"));

            OrganizationRole organizationRole = _organizationRoleService
                           .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId,
                               asNoTracking: true,
                               orgRole => orgRole.Organization,
                               orgRole => orgRole.SystemGroupMemberShips);

            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError($"OrganizationRole not found"));

            SystemGroup systemGroup = _systemGroupService
                                       .Get<SystemGroup>(s => s.Code == "wr_master",
                                               asNoTracking: true);
            if (systemGroup == null)
                return BadRequest("SystemGroup not found");

            IEnumerable<ReportTemplate> reportTemplates = _reportTemplateService
                                        .GetAll<ReportTemplate>(
                                                asNotracking: true,
                                                includes: t => t.TemplateOrganizations);

            if (!organizationRole.IsMemberOf(systemGroup))
                reportTemplates = reportTemplates
                        .Where(template => template.IsAccessible(organizationRole.Organization));

            return Ok(_typeAdapter.Adapt<IEnumerable<ShortReportTemplateVM>>(reportTemplates));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <param name="settingsVM"></param>
        /// <returns></returns>
        [HttpPut("reports/{reportId}/settings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> SetTemplateSettings([FromRoute] string orgRoleId, [FromRoute] string reportId, [FromBody] ReportTemplateSettingsVM settingsVM)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                  new BadRequestError($"{nameof(orgRoleId)} Parameter cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest,
                  new BadRequestError($"{nameof(orgRoleId)} Parameter cant be null"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = await GetCurrentUser();

            ReportTemplateSettingsDto settingsDto = new ReportTemplateSettingsDto
            {
                UserId = user.Id,
                OrgRoleId = orgRoleId,
                ReportId = reportId,
                Settings = settingsVM.Settings
            };

            try
            {
                _reportService.AddOrUpdateTemplateSettings(settingsDto);

                _unitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                 new BadRequestError(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("reports/{reportId}/settings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportTemplateSettingsVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> GetTemplateSettings(
            [FromRoute] string orgRoleId,
            [FromRoute] string reportId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError($"{nameof(orgRoleId)} cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError($"{nameof(reportId)} cant be null"));

            User user = await GetCurrentUser();
            try
            {
                ReportTemplateSettingsDto settingsDto = new ReportTemplateSettingsDto
                {
                    UserId = user.Id,
                    OrgRoleId = orgRoleId,
                    ReportId = reportId
                };

                JObject settings = _reportService.GetTemplateSettings(settingsDto);

                return StatusCode(StatusCodes.Status200OK, new ReportTemplateSettingsVM { Settings = settings });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                 new BadRequestError(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new InternalServerError(ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpGet("reports/{reportId}/templates/general-info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportTemplateInfoVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public IActionResult GetTemplateGeneralInfo(
            [FromRoute] string orgRoleId,
            [FromRoute] string reportId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError($"{nameof(orgRoleId)} cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                new BadRequestError($"{nameof(reportId)} cant be null"));

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(
                                                                orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                   new BadRequestError($"organizationRole not found"));

            Report report = _reportService.GetReportById<Report>(reportId,
                                                                 asNoTracking: true,
                                                                 r => r.ReportPermissions);
            if (report == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"report not found"));

            string organizationId = report.ReportPermissions.Single().OrganizationId;

            Organization organization = _organizationService.Get<Organization>(
                                                                org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"Organization not found"));

            ShortReportTemplateVM shortReportTemplate = _reportTemplateService.GetTemplateById<ShortReportTemplateVM>(
                                                                            report.TemplateId,
                                                                            asNotracking: true,
                                                                            t => t.CreatedBy);
            if (shortReportTemplate == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"ReportTemplate not found"));

            ReportTemplateInfoVM reportTemplateInfo = new ReportTemplateInfoVM
            {
                Organization = organization.OrganizationName,
                Template = shortReportTemplate
            };

            return StatusCode(StatusCodes.Status200OK, reportTemplateInfo);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpGet("reports/{reportId}/statements")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<SignableStatementVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public IActionResult GetStatements([FromRoute] string orgRoleId, [FromRoute] string reportId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError($"{nameof(orgRoleId)} cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                new BadRequestError($"{nameof(reportId)} cant be null"));

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(
                                                               orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                   new BadRequestError($"organizationRole not found"));

            Report report = _reportService.GetReportById<Report>(reportId,
                                                                 asNoTracking: true,
                                                                 r => r.ReportPermissions);
            if (report == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"report not found"));

            if (report.TemplateId == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"ReportTemplate not found"));

            string organizationId = report.ReportPermissions.Single().OrganizationId;

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"Organization not found"));

            IEnumerable<SignableStatementVM> statements = _signableStatementService.GetAll<SignableStatementVM>(
                                                                 statetmant => statetmant.OrganizationId == organization.Id &&
                                                                               statetmant.ReportTemplateId == report.TemplateId,
                                                                 asNoTracking: true,
                                                                 statement => statement.StatementSigners,
                                                                 statement => statement.StatementCategory);


            return StatusCode(StatusCodes.Status200OK, statements);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpPut("reports/{reportId}/statements")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<SignableStatementVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> AddOrUpdateStatements([FromRoute] string orgRoleId, [FromRoute] string reportId,
            [FromBody] IEnumerable<SignableStatementVM> signableStatementVMs)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError($"{nameof(orgRoleId)} cant be null"));

            if (string.IsNullOrEmpty(reportId) || string.IsNullOrWhiteSpace(reportId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                new BadRequestError($"{nameof(reportId)} cant be null"));

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            Report report = _reportService.GetReportById<Report>(reportId,
                                                                 asNoTracking: true,
                                                                 r => r.ReportPermissions);
            if (report == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"report not found"));

            if (report.TemplateId == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"ReportTemplate not found"));

            string organizationId = report.ReportPermissions.Single().OrganizationId;

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new BadRequestError($"Organization not found"));

            User user = await GetCurrentUser();

            ICollection<SignableStatementDto> signableStatementDtos = _typeAdapter.Adapt<ICollection<SignableStatementDto>>(signableStatementVMs);

            try
            {
                _signableStatementService.AddOrUpdate(report, organization, user, signableStatementDtos);

                _signableStatementService.UnitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                                   new NotFoundError(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   new InternalServerError(ex.Message));
            }
        }
    }
}
