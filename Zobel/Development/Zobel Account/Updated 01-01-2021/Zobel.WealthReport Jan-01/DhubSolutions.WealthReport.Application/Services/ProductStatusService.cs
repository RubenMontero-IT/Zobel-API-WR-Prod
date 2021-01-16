using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class ProductStatusService : WealthReportService<ProductStatus>, IProductStatusService
    {
        public ProductStatusService(ITypeAdapter typeAdapter, IWealthReportRepository<ProductStatus> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}
