using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Api.Errors;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Application.Dtos;
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
    public class ProductController : ControllerBase
    {
        private readonly ITypeAdapter _typeAdapter;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationRoleService _organizationRoleService;
        private readonly IProductService _productService;
        private readonly ICurrencyService _currencyService;
        private readonly IRegionService _regionService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProcessManagerService _processManagerService;
        private readonly IAccountService _accountService;
        private readonly ISectionService _sectionService;
        private readonly IAssetClassService _assetClassService;
        private readonly ILiquidityService _liquidityService;
        private readonly IProductStyleService _productStyleService;
        private readonly IProductStatusService _productStatusService;
        private readonly ICapitalTransactionTypeService _capitalTransactionTypeService;
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IPortfolioCategoryService _portfolioCategoryService;

        public ProductController(
            ITypeAdapter typeAdapter,
            IOrganizationService organizationService,
            IOrganizationRoleService organizationRoleService,
            IProductService productService,
            ICurrencyService currencyService,
            IRegionService regionService,
            IProductTypeService productTypeService,
            IAccountService accountService,
            IAssetClassService assetClassService,
            ISectionService sectionService,
            IProductStatusService productStatusService,
            ILiquidityService liquidityService,
            IProductStyleService productStyleService,
            ICapitalTransactionTypeService capitalTransactionTypeService,
            ITransactionTypeService transactionTypeService,
            IPortfolioCategoryService portfolioCategoryService,
            IProcessManagerService processManagerService)
        {
            _typeAdapter = typeAdapter;
            _organizationService = organizationService;
            _organizationRoleService = organizationRoleService;
            _productService = productService;
            _currencyService = currencyService;
            _regionService = regionService;
            _productTypeService = productTypeService;
            _accountService = accountService;
            _assetClassService = assetClassService;
            _sectionService = sectionService;
            _productStatusService = productStatusService;
            _liquidityService = liquidityService;
            _productStyleService = productStyleService;
            _capitalTransactionTypeService = capitalTransactionTypeService;
            _transactionTypeService = transactionTypeService;
            _portfolioCategoryService = portfolioCategoryService;
            _processManagerService = processManagerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAllProducts([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                              .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId,
                                                               asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<dynamic> products = await _productService.GetAllProducts(organization);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name="productId"></param>      
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProduct([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            if (string.IsNullOrEmpty(productId) || string.IsNullOrWhiteSpace(productId))
                return BadRequest("ProductId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            dynamic product = await _productService.GetProduct(organization, productId);

            if (product == null)
                return NotFound("Products not found");
            else
            {
                if (product.parentPortfolio != null)
                    product.parentPortfolio = product.parentPortfolio.Split(',');
            }

            return Ok(product);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name="productId"></param>        
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product/{productId}/productTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductTransactions([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("OrganizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("OrganizationId Parameter cant be null");

            if (string.IsNullOrEmpty(productId) || string.IsNullOrWhiteSpace(productId))
                return BadRequest("ProductId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<dynamic> products = await _productService.GetProductTransactions(organization, productId);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name="productId"></param>        
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product/{productId}/productHistoricalPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductHistoricalPrices([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("OrganizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("OrganizationId Parameter cant be null");

            if (string.IsNullOrEmpty(productId) || string.IsNullOrWhiteSpace(productId))
                return BadRequest("ProductId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<dynamic> products = await _productService.GetProductHistoricalPrices(organization, productId);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/currencies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllCurrencies([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");


            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> currencies = _currencyService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(currencies);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/regions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllRegions([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");


            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> regions = _regionService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(regions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/accounts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllAccounts([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");


            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> accounts = _accountService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(accounts);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/capitalTransactionTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllCapitalTransactionTypes([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");


            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> capitalTransactionTypes = _capitalTransactionTypeService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(capitalTransactionTypes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/transactionTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllTransactionTypes([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");


            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");


            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> transactionTypes = _transactionTypeService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(transactionTypes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/assetClasses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllAssetClasses([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> assetClasses = _assetClassService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(assetClasses);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/sections")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllSections([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> sections = _sectionService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(sections);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/productStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllProductStatus([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> productStatus = _productStatusService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(productStatus);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/liquidity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllLiquidity([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> liquidity = _liquidityService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(liquidity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost("organizations/{organizationId}/liquidity/{liquidityValue}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(InternalServerError)))]
        public async Task<IActionResult> CreateLiquidity([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string liquidityValue)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("The organizationRole Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("The organization Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));            
            try
            {
                dynamic liquidity = _liquidityService.CreateLiquidity(organization, liquidityValue);               
                return StatusCode(StatusCodes.Status201Created, new { id = liquidity.Id });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/productStyles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllProductStyles([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> productStyles = _productStyleService.GetAll<MetadataVM>(organization, asNoTracking: true);
            return Ok(productStyles);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost("organizations/{organizationId}/productStyles/{styleValue}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(InternalServerError)))]
        public async Task<IActionResult> CreateProductStyles([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string styleValue)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("The organizationRole Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("The organization Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));
            try
            {
                dynamic productStyles = _productStyleService.CreateProductStyle(organization, styleValue);
                return StatusCode(StatusCodes.Status201Created, new { id = productStyles.Id });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/product-metadata/portfolioCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MetadataVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetAllPortfolioCategories([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("organizationRoleId Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("organizationId Parameter cant be null");

            OrganizationRole organizationRole = _organizationRoleService
                                            .Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return NotFound($"OrganizationRole not found");

            Organization organization = _organizationService
                                            .Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return NotFound("Organization not found");

            IEnumerable<MetadataVM> portfolioCategories = _portfolioCategoryService.GetAll<MetadataVM>(organization, portfolioCategory => portfolioCategory.CategoryCode != null, asNoTracking: true);                       
            return Ok(portfolioCategories.OrderBy(portfolio => portfolio.Value));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost("organizations/{organizationId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(InternalServerError)))]
        public async Task<IActionResult> CreateProduct ([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromBody] ProductCreationVM productCreationVM)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("The organizationRole Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("The organization Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            ProductCreationDto productCreation = _typeAdapter.Adapt<ProductCreationDto>(productCreationVM);
            try
            {

                dynamic product = await _productService.CreateProduct(organization, productCreation);
                return StatusCode(StatusCodes.Status201Created, new { id = product.ProductID });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost("organizations/{organizationId}/uploadHistoricalPrices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(InternalServerError)))]
        public async Task<IActionResult> UploadHistoricalPrices([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromBody] IEnumerable<UploadHistoricalPricesVM> uploadHistoricalPricesVM)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest("The organizationRole Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest("The organization Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            IEnumerable<UploadHistoricalPricesDto> uploadJson = _typeAdapter.Adapt<IEnumerable<UploadHistoricalPricesDto>>(uploadHistoricalPricesVM);

            try
            {
                _productService.UploadHistoricalPrices(organization, uploadJson);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }
            return Ok();

        }


        ///// <summary>
        ///// 
        ///// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name="productId"></param>
        /// <param name="productUpdateVM"></param>
        /// <returns></returns>
        [HttpPut("organizations/{organizationId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public async Task<IActionResult> UpdateProduct([FromRoute] string orgRoleId, [FromRoute] string organizationId, [FromRoute] string productId, [FromBody] ProductUpdateVM productUpdateVM)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest($"The {nameof(orgRoleId)} Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest($"The {nameof(organizationId)} Parameter cant be null");

            if (string.IsNullOrEmpty(productId) || string.IsNullOrWhiteSpace(productId))
                return BadRequest($"The {nameof(productId)} Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            dynamic product = await _productService.GetProduct(organization, productId);
            if (product == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Product not found"));

            ProductUpdateDto productUpdate = _typeAdapter.Adapt<ProductUpdateDto>(productUpdateVM);

            try
            {
                _productService.UpdateProduct(organization, productId, productUpdate);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(e.Message));
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("organizations/{organizationId}/MetricRecalc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public IActionResult MetricRecalc([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest($"The {nameof(orgRoleId)} Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest($"The {nameof(organizationId)} Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            try
            {
                _productService.MetricRecalc(organization);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/MetricStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public IActionResult GetLastMetricCalculationExecutionStatus([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest($"The {nameof(orgRoleId)} Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest($"The {nameof(organizationId)} Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            try
            {
                dynamic value = _productService.GetLastMetricCalculationExecutionStatus(organization);

                return StatusCode(StatusCodes.Status200OK, value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgRoleId"></param>
        /// <param name="organizationId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("organizations/{organizationId}/LastMetricUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime?))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerError))]
        public IActionResult GetLastSuccessfulMetricCalculation([FromRoute] string orgRoleId, [FromRoute] string organizationId)
        {
            if (string.IsNullOrEmpty(orgRoleId) || string.IsNullOrWhiteSpace(orgRoleId))
                return BadRequest($"The {nameof(orgRoleId)} Parameter cant be null");

            if (string.IsNullOrEmpty(organizationId) || string.IsNullOrWhiteSpace(organizationId))
                return BadRequest($"The {nameof(organizationId)} Parameter cant be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrganizationRole organizationRole = _organizationRoleService.Get<OrganizationRole>(orgRole => orgRole.Id == orgRoleId, asNoTracking: true);
            if (organizationRole == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("OrganizationRole not found"));

            Organization organization = _organizationService.Get<Organization>(org => org.Id == organizationId, asNoTracking: true);
            if (organization == null)
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError("Organization not found"));

            try
            {
                dynamic value = _productService.GetLastSuccessfulMetricCalculation(organization);

                return StatusCode(StatusCodes.Status200OK, value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}