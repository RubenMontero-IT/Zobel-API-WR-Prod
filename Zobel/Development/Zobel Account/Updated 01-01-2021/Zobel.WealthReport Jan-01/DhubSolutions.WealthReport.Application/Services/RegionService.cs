using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class RegionService : WealthReportService<Region>, IRegionService
    {
        public RegionService(ITypeAdapter typeAdapter, IWealthReportRepository<Region> repository)
            : base(typeAdapter, repository)
        { }
    }
}
