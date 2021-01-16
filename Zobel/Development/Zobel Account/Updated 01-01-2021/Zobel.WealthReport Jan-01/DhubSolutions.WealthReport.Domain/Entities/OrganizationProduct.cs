using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class OrganizationProduct : BaseEntity
    {
        public OrganizationProduct() : base()
        {
            OrganizationProductRegistries = new HashSet<OrganizationProductRegistry>();
            PortfolioCatOrgProds = new HashSet<PortfolioCatOrgProd>();
        }

        public string ProductID { get; set; }
        public string OrganizationID { get; set; }
        public double? EntryPrice { get; set; }
        public double? ExitPrice { get; set; }
        public double? InitNumberOfUnits { get; set; }
        public double? InitialInvestmentEUR { get; set; }
        public double? ExitNumberOfUnits { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DisplayName { get; set; }

        public Product Product { get; set; }
        public ICollection<OrganizationProductRegistry> OrganizationProductRegistries { get; set; }
        public ICollection<PortfolioCatOrgProd> PortfolioCatOrgProds { get; set; }
    }
}
