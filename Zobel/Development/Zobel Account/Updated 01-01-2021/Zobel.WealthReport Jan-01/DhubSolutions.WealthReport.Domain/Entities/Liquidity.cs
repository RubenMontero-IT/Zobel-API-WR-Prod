using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Liquidity : BaseEntity
    {
        public Liquidity() : base()
        {
            LiquidityProducts = new HashSet<LiquidityProduct>();
        }
        public string LiquidityValue { get; set; }
        public ICollection<LiquidityProduct> LiquidityProducts { get; set; }

    }
}
