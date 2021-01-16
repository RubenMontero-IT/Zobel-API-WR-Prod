using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class OrganizationProductExtendedRegistry : BaseEntity
    {
        public OrganizationProductExtendedRegistry() : base()
        {
        }

        public string ProductID { get; set; }
        public string ISIN { get; set; }
        public string SEDOL { get; set; }
        public string CUSIP { get; set; }
        public string Ticker { get; set; }
        public string BaseCurrencyId { get; set; }
        public string BloombergID { get; set; }
        public string OrganizationID { get; set; }
        public DateTime Date { get; set; }
        public double? RoR { get; set; }
        public string ITDRoR { get; set; }
        public string ITDPL { get; set; }
        public double? PL { get; set; }
        public double? PLFX { get; set; }
        public string ITDPLFX { get; set; }
        public string Vol { get; set; }
        public string ITDVol { get; set; }
        public string VAMI { get; set; }
        public double? NAV { get; set; }
        public string MainCurrencyId { get; set; }

        public Currency BaseCurrency { get; set; }
        public Currency MainCurrency { get; set; }
        public OrganizationProductRegistry OrganizationProductRegistry { get; set; }
    }
}
