using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class ProductStyleService : WealthReportService<ProductStyle>, IProductStyleService
    {
        public ProductStyleService(ITypeAdapter typeAdapter, IWealthReportRepository<ProductStyle> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="liquidityValue"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>returns the product identifier created</returns>
        public ProductStyle CreateProductStyle(Organization organization, string styleValue)
        {
            ProductStyle productStyle = Create<ProductStyle>();
            productStyle.ProductStyleName = styleValue;

            return Add<ProductStyle>(organization, productStyle);
        }
    }
}
