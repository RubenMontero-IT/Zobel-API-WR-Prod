using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class LiquidityService : WealthReportService<Liquidity>, ILiquidityService
    {
        public LiquidityService(ITypeAdapter typeAdapter, IWealthReportRepository<Liquidity> reportRepository)
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
        public Liquidity CreateLiquidity(Organization organization, string liquidityValue)
        {
            Liquidity liquidity = Create<Liquidity>();
            liquidity.LiquidityValue = liquidityValue;

            return Add<Liquidity>(organization, liquidity);
        }
    }
}
