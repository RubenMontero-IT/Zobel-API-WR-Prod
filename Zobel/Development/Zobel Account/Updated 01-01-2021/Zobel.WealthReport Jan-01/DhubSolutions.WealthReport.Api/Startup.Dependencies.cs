using DhubSolutions.Common.Application.Services.Admin;
using DhubSolutions.Common.Application.Services.Admin.Base;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Application.Adapters;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Core.Infrastructure.Data.Context;
using DhubSolutions.Reports.Application.Services.ReportManager;
using DhubSolutions.Reports.Application.Services.ReportManager.Base;
using DhubSolutions.Reports.Domain.Repositories.ReportManager;
using DhubSolutions.Reports.Domain.Services;
using DhubSolutions.Reports.Domain.Services.Base;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base;
using DhubSolutions.WealthReport.Api;
using DhubSolutions.WealthReport.Application.Services;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Repositories;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using DhubSolutions.WealthReport.Infrastructure.Data.Repositories;
using DhubSolutions.WealthReport.Infrastructure.Data.Repositories.Admin;
using DhubSolutions.WealthReport.Infrastructure.Data.Repositories.ReportManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Zobel.WealthReport.Api
{
    public partial class Startup
    {
        public void ConfigureDependencies(IConfiguration configuration, IServiceCollection services)
        {
            #region Zobel.Core Module

            services
               .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork<ProjectManagementDbContext>>()
               .AddScoped<IEntityFrameworkUnitOfWork, EntityFrameworkUnitOfWork<ProjectManagementDbContext>>()
               .AddScoped<IEntityFrameworkContextAccessor, WealthReportDbContextAccessor>()
               .AddScoped<ITypeAdapter, AutoMapperTypeAdapter>();

            #endregion

            #region Zobel.Report Module

            services
               .AddScoped<IReportService, ReportService>()
               .AddScoped<IReportTemplateService, ReportTemplateService>()
               .AddScoped<IReportManagerService, ReportManagerService>()
               .AddScoped<IReportTemplateManagerService, ReportTemplateManagerService>();

            services
                .RegisterAllTypes<IInstructionProcessorFactory>(assemblies.ToArray(), ServiceLifetime.Scoped);

            services
                .AddScoped<IInstructionProcessorFactoryProvider, InstructionProcessorFactoryProvider>()
                .AddScoped<IInstructionProcessorFactoryProxy, InstructionProcessorFactoryProxy>()
                .AddScoped<IWealthReportDataRepository, WealthReportDataRepository>(sp =>
                {
                    return new WealthReportDataRepository(configuration);
                });

            services
                .AddScoped<IReportRepository, ReportRepository>()
                .AddScoped<IReportElementRepository, ReportElementRepository>()
                .AddScoped<IReportTemplateRepository, ReportTemplateRepository>()
                .AddScoped<IReportTemplateElementRepository, ReportTemplateElementRepository>()
                .AddScoped<IReportTemplatePermissionRepository, ReportTemplatePermissionRepository>()
                .AddScoped<IReportTemplateElementPermissionRepository, ReportTemplateElementPermissionRepository>();


            #endregion

            #region Zobel.Common Module

            services
                .AddScoped<IPermissionService, PermissionService>()
                .AddScoped<IOrganizationService, OrganizationService>()
                .AddScoped<IOrganizationRoleService, OrganizationRoleService>()
                .AddScoped<ISystemGroupService, SystemGroupService>()
                .AddScoped<IUserRoleOrgService, UserRoleOrgService>();

            services
                .AddScoped<IPermissionRepository, PermissionRepository>()
                .AddScoped<IOrganizationRepository, OrganizationRepository>()
                .AddScoped<IOrganizationRoleRepository, OrganizationRoleRepository>()
                .AddScoped<ISystemGroupRepository, SystemGroupRepository>()
                .AddScoped<IUserRoleOrgRepository, UserRoleOrgRepository>();

            #endregion

            #region Zobel.WealthReport Module

            services
                .AddSingleton<IProcessManagerService, ProcessManagerService>()
                .AddSingleton<IOrganizationManagerService, OrganizationManagerService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICurrencyService, CurrencyService>()
                .AddScoped<IRegionService, RegionService>()
                .AddScoped<IProductTypeService, ProductTypeService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAssetClassService, AssetClassService>()
                .AddScoped<ISectionService, SectionService>()
                .AddScoped<IProductStatusService, ProductStatusService>()
                .AddScoped<IPortfolioCategoryService, PortfolioCategoryService>()                
                .AddScoped<ILiquidityService, LiquidityService>()
                .AddScoped<IProductStyleService, ProductStyleService>()
                .AddScoped<ICapitalTransactionTypeService, CapitalTransactionTypeService>()
                .AddScoped<ITransactionTypeService, TransactionTypeService>()
                .AddScoped<IOrganizationWRService, OrganizationWRService>()
                .AddScoped<ISignableStatementService, SignableStatementService>()
                .AddScoped<IStatementCategoryService, StatementCategoryService>();


            services
                .AddSingleton<IConnectionByOrganizationRepository, ConnectionByOrganizationRepository>()
                .AddScoped(typeof(IWealthReportRepository<>), typeof(WealthReportRepository<>))
                .AddScoped(typeof(IWealthReportReadOnlyRepository<>), typeof(WealthReportReadOnlyRepository<>))
                .AddScoped<ITemplateSettingsRepository, TemplateSettingsRepository>()
                .AddScoped<IOrganizationWealthReportRepository, OrganizationWealthReportRepository>()
                .AddScoped<ISignableStatementRepository, SignableStatementRepository>()
                .AddScoped<IStatementCategoryRepository, StatementCategoryRepository>()
                .AddScoped<IStatementSignerRepository,StatementSignerRepository>()
                .AddScoped<IQueryableRepository, QueryableRepository>();


            #endregion
        }
    }
}