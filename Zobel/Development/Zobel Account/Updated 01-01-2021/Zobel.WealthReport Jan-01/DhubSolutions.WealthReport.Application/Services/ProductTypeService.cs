using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class ProductTypeService : WealthReportService<ProductType>, IProductTypeService
    {
        public ProductTypeService(ITypeAdapter typeAdapter, IWealthReportRepository<ProductType> reportRepository)
            : base(typeAdapter, reportRepository)
        {

        }
    }
}
