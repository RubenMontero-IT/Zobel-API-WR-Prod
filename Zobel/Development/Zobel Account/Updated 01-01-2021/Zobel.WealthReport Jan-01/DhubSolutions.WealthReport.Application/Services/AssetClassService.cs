using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class AssetClassService : WealthReportService<AssetClass>, IAssetClassService
    {
        public AssetClassService(ITypeAdapter typeAdapter, IWealthReportRepository<AssetClass> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}
