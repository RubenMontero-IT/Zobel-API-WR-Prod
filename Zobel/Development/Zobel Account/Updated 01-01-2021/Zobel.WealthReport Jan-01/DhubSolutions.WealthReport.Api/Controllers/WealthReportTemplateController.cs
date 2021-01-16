using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Dtos.ReportManager;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.WealthReport.Api.Controllers.Base;
using DhubSolutions.WealthReport.Api.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Api.Controllers
{
    [Route("api/[controller]/{orgRoleId}")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WealthReportTemplateController : BaseController
    {
        private readonly ITypeAdapter _typeAdapter;
        private readonly IOrganizationRoleService _organizationRoleService;
        private readonly IReportTemplateService _reportTemplateService;

        public WealthReportTemplateController(
            IUnitOfWork unitofWork,
            ITypeAdapter typeAdapter,
            IOrganizationRoleService organizationRoleService,
            IReportTemplateService reportTemplateService,
            UserManager<User> userManager) : base(unitofWork, userManager)
        {
            _typeAdapter = typeAdapter;
            _organizationRoleService = organizationRoleService;
            _reportTemplateService = reportTemplateService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="reportTemplateVM"></param>
        /// <returns></returns>
        [HttpPost("Template")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> CreateTemplate([FromRoute] string orgRoleId, [FromBody] ReportTemplateVM reportTemplateVM)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))

                return StatusCode(StatusCodes.Status400BadRequest,
                                  new BadRequestError("organizationRoleId Parameter cant be null"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService
                                               .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound,
                                  new NotFoundError($"OrganizationRole not found"));

            User user = await GetCurrentUser();
            if (user is null)
                return StatusCode(StatusCodes.Status404NotFound,
                                  new NotFoundError("Current user not found"));

            ReportTemplateDto reportTemplateDto = _typeAdapter.Adapt<ReportTemplateVM, ReportTemplateDto>(reportTemplateVM);

            var creationReportTemplate = new ReportTemplateCreationDto()
            {
                UserId = user.Id,
                ReportTemplate = reportTemplateDto
            };

            ShortReportTemplateVM reportTemplate = _reportTemplateService
                            .CreateTemplate<ShortReportTemplateVM>(creationReportTemplate);

            if (_unitOfWork.SaveChanges() < 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                                  new BadRequestError("Error when trying to insert a template"));

            return StatusCode(StatusCodes.Status201Created, reportTemplate.Id);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpGet("Template/{templateId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportTemplateVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> GetTemplate([FromRoute] string orgRoleId, [FromRoute] string templateId)
        {
            if (string.IsNullOrEmpty(templateId) || string.IsNullOrWhiteSpace(templateId))
                return StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError("Id Parameter cant be null"));

            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                StatusCode(StatusCodes.Status400BadRequest,
                                 new BadRequestError("organizationRoleId Parameter cant be null"));

            OrganizationRole organizationRole = _organizationRoleService
                    .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            User user = await GetCurrentUser();
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("current User not found"));

            ReportTemplateVM reportTemplate = _reportTemplateService.
                                GetTemplateById<ReportTemplateVM>(templateId, asNotracking: true);
            if (reportTemplate == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("template not found"));

            return StatusCode(StatusCodes.Status200OK, reportTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrgRoleId"></param>
        /// <param name="templateId"></param>
        /// <param name="reportTemplateVM"></param>
        /// <returns></returns>
        [HttpPut("Template/{templateId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public async Task<IActionResult> UpdateTemplate([FromRoute] string OrgRoleId, [FromRoute] string templateId, [FromBody] ReportTemplateVM reportTemplateVM)
        {
            if (string.IsNullOrEmpty(OrgRoleId) || string.IsNullOrWhiteSpace(OrgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest,
                  new BadRequestError($"{nameof(OrgRoleId)} Parameter cant be null"));

            if (string.IsNullOrEmpty(templateId) || string.IsNullOrWhiteSpace(templateId))
                return StatusCode(StatusCodes.Status400BadRequest,
                  new BadRequestError($"{nameof(templateId)} Parameter cant be null"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService
                                               .Get<OrganizationRole>(orgRole => orgRole.Id == OrgRoleId);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound,
                                  new NotFoundError($"OrganizationRole not found"));

            User user = await GetCurrentUser();
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("current User not found"));

            ReportTemplateDto reportTemplateDto = _typeAdapter.Adapt<ReportTemplateVM, ReportTemplateDto>(reportTemplateVM);

            var reportTemplateUpdateDto = new ReportTemplateUpdateDto
            {
                UserId = user.Id,
                TemplateId = templateId,
                ReportTemplate = reportTemplateDto
            };

            try
            {
                _reportTemplateService.UpdateReportTemplate(reportTemplateUpdateDto);

                if (_unitOfWork.SaveChanges() < 0)
                    return StatusCode(StatusCodes.Status400BadRequest,
                                      new BadRequestError("Error when trying to update a template"));

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                  new BadRequestError(ex.Message));
            }



        }

    }
}