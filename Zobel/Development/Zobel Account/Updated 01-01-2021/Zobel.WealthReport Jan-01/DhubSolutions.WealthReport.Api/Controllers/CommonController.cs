using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Api.Errors;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Application.Services.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DhubSolutions.WealthReport.Api.Controllers
{
    [Route("api/[controller]/{orgRoleId}")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommonController : ControllerBase
    {
        private readonly IOrganizationWRService _organizationWRService;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationRoleService _organizationRoleService;

        public CommonController(
            IOrganizationWRService organizationWRService,
            IOrganizationService organizationService,
            IOrganizationRoleService organizationRoleService)
        {
            _organizationWRService = organizationWRService;
            _organizationService = organizationService;
            _organizationRoleService = organizationRoleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/portafolio-info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationGeneralInfoVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public IActionResult GetOganizationGeneralInfo([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("organizationRoleId Parameter cant be null"));

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return StatusCode(StatusCodes.Status400BadRequest, new BadRequestError("organizationId Parameter cant be null"));

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError($"OrganizationRole not found"));

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            OrganizationGeneralInfoVM organizationGeneralInfo = _organizationWRService
                .Get<OrganizationGeneralInfoVM>(organization, org => org.Id == organizationId, asNoTracking: true);

            if (organizationGeneralInfo == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("General Information not found"));

            return StatusCode(StatusCodes.Status200OK, organizationGeneralInfo);

        }

    }
}