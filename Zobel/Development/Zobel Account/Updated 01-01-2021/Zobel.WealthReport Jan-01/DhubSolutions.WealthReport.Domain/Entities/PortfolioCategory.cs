using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class PortfolioCategory : BaseEntity
    {
        public PortfolioCategory() : base()
        {
            PortfolioCatOrgProds = new HashSet<PortfolioCatOrgProd>();
        }
        public string PortfolioCategoryName { get; set; }
        public int? PrevIdentifier { get; set; }
        public string Description { get; set; }
        public string CategoryCode { get; set; }
        public string FirmAUM { get; set; }
        public string FundAUM { get; set; }
        public string Employees { get; set; }
        public string RegisteredOffice { get; set; }
        public string ProductStyleID { get; set; }
        public string ManagementFee { get; set; }
        public string PerformanceFee { get; set; }
        public string PortfolioManager { get; set; }
        public string LiquidityID { get; set; }
        public string Strategy { get; set; }
        public bool? VisibleDetails { get; set; }
        public bool? VisibleFacts { get; set; }
        public string BaseCurrency { get; set; }

        public ICollection<PortfolioCatOrgProd> PortfolioCatOrgProds { get; set; }
    }
}
