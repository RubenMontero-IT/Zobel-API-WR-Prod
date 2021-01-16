using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface IProductStyleService : IWealthReportService<ProductStyle>
    {
        ProductStyle CreateProductStyle(Organization organization, string styleValue);
    }
}
