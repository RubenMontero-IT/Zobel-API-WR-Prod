using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Domain.Entities;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface ILiquidityService : IWealthReportService<Liquidity>
    {
        Liquidity CreateLiquidity(Organization organization, string liquidityValue);
    }
}
