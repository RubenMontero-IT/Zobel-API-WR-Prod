using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class PortfolioCatOrgProd : BaseEntity
    {
        public PortfolioCatOrgProd() : base()
        {

        }
        public string PortfolioCategoryID { get; set; }
        public string ProductID { get; set; }
        public string OrganizationID { get; set; }

        public OrganizationProduct OrganizationProduct { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; }
    }
}
