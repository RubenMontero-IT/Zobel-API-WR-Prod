using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class LiquidityProduct : BaseEntity
    {
        public LiquidityProduct() : base()
        {
        }

        public string ProductID { get; set; }
        public string LiquidityID { get; set; }

        public Liquidity Liquidity { get; set; }
        public Product Product { get; set; }
    }
}
