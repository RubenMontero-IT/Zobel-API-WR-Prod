using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class PorfolioCategory : BaseEntity
    {
        public PorfolioCategory() : base()
        {
            PortfolioCatOrgProds = new HashSet<PortfolioCatOrgProd>();
        }
        public string PortfolioCategory { get; set; }

        public ICollection<PortfolioCatOrgProd> PortfolioCatOrgProds { get; set; }
    }
}
