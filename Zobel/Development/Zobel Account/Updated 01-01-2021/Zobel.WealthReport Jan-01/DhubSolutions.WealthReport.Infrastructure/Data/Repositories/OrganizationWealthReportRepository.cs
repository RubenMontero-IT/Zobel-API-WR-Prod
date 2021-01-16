using DhubSolutions.Core.Infrastructure.Data.Context;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class OrganizationWealthReportRepository : WealthReportReadOnlyRepository<Organization_WR>, IOrganizationWealthReportRepository
    {
        public OrganizationWealthReportRepository(IEntityFrameworkContextAccessor contextAccessor, IConnectionByOrganizationRepository repository)
            : base(contextAccessor, repository)
        {
        }
    }
}
