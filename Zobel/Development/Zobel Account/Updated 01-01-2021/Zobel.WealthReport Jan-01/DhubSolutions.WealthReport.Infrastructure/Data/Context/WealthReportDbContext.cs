using DhubSolutions.WealthReport.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Context
{
    public class WealthReportDbContext : DbContext
    {

        public WealthReportDbContext(DbContextOptions<WealthReportDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CustomModel(modelBuilder);
        }

        void CustomModel(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionTypeConfig());
            modelBuilder.ApplyConfiguration(new CapitalTransactionTypeConfig());
            modelBuilder.ApplyConfiguration(new ProductStatusConfig());
            modelBuilder.ApplyConfiguration(new SectionConfig());
            modelBuilder.ApplyConfiguration(new AssetClassConfig());
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new CurrencyConfig());
            modelBuilder.ApplyConfiguration(new MetricsCategoriesConfig());
            modelBuilder.ApplyConfiguration(new PortfolioCategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductTypeConfig());
            modelBuilder.ApplyConfiguration(new RegionConfig());
            modelBuilder.ApplyConfiguration(new ProductStyleConfig());
            modelBuilder.ApplyConfiguration(new OrganizationProductConfig());
            modelBuilder.ApplyConfiguration(new OrganizationProductRegistryConfig());
            modelBuilder.ApplyConfiguration(new PortfolioCatOrgProdConfig());
            modelBuilder.ApplyConfiguration(new ProductRegistryConfig());
            modelBuilder.ApplyConfiguration(new OrganizationProductExtendedRegistryConfigs());
            modelBuilder.ApplyConfiguration(new FrequencyConfig());
            modelBuilder.ApplyConfiguration(new FXRateConfig());
            modelBuilder.ApplyConfiguration(new LiquidityConfig());
            modelBuilder.ApplyConfiguration(new LiquidityProductConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new ProductFrequencyConfig());
            modelBuilder.ApplyConfiguration(new ProductExtendedRegistryConfig());
            modelBuilder.ApplyConfiguration(new Organization_WRConfig());
        }
    }
}
