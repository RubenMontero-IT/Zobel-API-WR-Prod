using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ProductExtendedRegistry : BaseEntity
    {
        public ProductExtendedRegistry() : base()
        {

        }
        public string ProductID { get; set; }
        public DateTime Date { get; set; }
        public string MainCurrencyId { get; set; }
        public string SEDOL { get; set; }
        public string CUSIP { get; set; }
        public string ISIN { get; set; }
        public string Ticker { get; set; }
        public string BloombergID { get; set; }
        public double Price { get; set; }
        public string BaseCurrencyId { get; set; }
        public double? Vol { get; set; }
        public double? ITDVol { get; set; }
        public double? RoR { get; set; }
        public double? VAMI { get; set; }
        public double? ITDRoR { get; set; }
        public double? NAV { get; set; }
        public double? PL { get; set; }
        public double? ITDPL { get; set; }

        public Currency BaseCurrency { get; set; }
        public Currency MainCurrency { get; set; }
        public ProductRegistry ProductRegistry { get; set; }
    }
}
