using DhubSolutions.Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Context
{
    public class WealthReportDbContextAccessor : IEntityFrameworkContextAccessor<WealthReportDbContext>,
                                                 IEntityFrameworkContextAccessor,
                                                 IDesignTimeDbContextFactory<WealthReportDbContext>
    {
        private readonly IConfiguration _configuration;
        public WealthReportDbContextAccessor()
        {

        }
        public WealthReportDbContextAccessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WealthReportDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WealthReportDbContext>();
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
#endif
            optionsBuilder.UseSqlServer(args.Length > 0 ? args[0] : "server=.;database=wealth_store;trusted_connection=true;");
            return new WealthReportDbContext(optionsBuilder.Options);
        }

        public WealthReportDbContext GetContext(string nameConnectionString)
        {
            return CreateDbContext(new[] { _configuration.GetConnectionString(nameConnectionString) });
        }

        DbContext IEntityFrameworkContextAccessor<DbContext>.GetContext(string nameConnectionString)
        {
            return GetContext(nameConnectionString);
        }
    }
}
