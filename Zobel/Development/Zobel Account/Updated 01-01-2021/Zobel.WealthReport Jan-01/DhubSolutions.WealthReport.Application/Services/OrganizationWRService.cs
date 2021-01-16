using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class OrganizationWRService : WealthReportService<Organization_WR>, IOrganizationWRService
    {
        public OrganizationWRService(ITypeAdapter typeAdapter, IWealthReportRepository<Organization_WR> repository)
            : base(typeAdapter, repository)
        {
        }
    }
}
