using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product() : base()
        {
            LiquidityProducts = new HashSet<LiquidityProduct>();
            OrganizationProducts = new HashSet<OrganizationProduct>();
            ProductFrequencies = new HashSet<ProductFrequency>();
            ProductRegistries = new HashSet<ProductRegistry>();
        }

        public string SEDOL { get; set; }
        public string CUSIP { get; set; }
        public string ISIN { get; set; }
        public string Ticker { get; set; }
        public string BloombergID { get; set; }
        public string BloombergName { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string StyleId { get; set; }
        public string RegionId { get; set; }
        public string BaseCurrencyId { get; set; }
        public string ContactDetails { get; set; }
        public string AUM { get; set; }
        public string Employees { get; set; }
        public string ManagementFee { get; set; }
        public string PerformanceFee { get; set; }
        public bool? TakingBackEffect { get; set; }
        public string Strengh { get; set; }
        public string History { get; set; }
        public double? TotalUnitNumber { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? LastHistUpdate { get; set; }
        public string OtherID { get; set; }
        public string GeneralDescription { get; set; }
        public Currency BaseCurrency { get; set; }
        public Region Region { get; set; }
        public ProductType ProductType { get; set; }
        public Style Style { get; set; }
        public ICollection<LiquidityProduct> LiquidityProducts { get; set; }
        public ICollection<OrganizationProduct> OrganizationProducts { get; set; }
        public ICollection<ProductFrequency> ProductFrequencies { get; set; }
        public ICollection<ProductRegistry> ProductRegistries { get; set; }
    }
}
