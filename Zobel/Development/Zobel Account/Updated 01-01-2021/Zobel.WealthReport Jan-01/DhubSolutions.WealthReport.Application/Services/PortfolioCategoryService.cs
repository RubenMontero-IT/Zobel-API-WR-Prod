using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class PortfolioCategoryService : WealthReportService<PortfolioCategory>, IPortfolioCategoryService
    {
        public PortfolioCategoryService(ITypeAdapter typeAdapter, IWealthReportRepository<PortfolioCategory> reportRepository)
          : base(typeAdapter, reportRepository)
        {
        }
    }
}
