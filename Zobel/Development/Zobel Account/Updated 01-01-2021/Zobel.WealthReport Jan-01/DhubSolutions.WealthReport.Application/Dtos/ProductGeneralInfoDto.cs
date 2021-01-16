using System;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductGeneralInfoDto
    { 
        public string SEDOL { get; set; }

        public string CUSIP { get; set; }

        public string ISIN { get; set; }

        public string BloombergTicker { get; set; }       

        public string DisplayName { get; set; }

        public string AssetClassID { get; set; }

        public string Strategy { get; set; }

        public string RegionID { get; set; }

        public string BaseCurrency { get; set; }

        public string FirmAUM { get; set; }

        public string Employees { get; set; }

        public string ManagementFee { get; set; }

        public string PerformanceFee { get; set; }

        public bool? TakingBackEffect { get; set; }

        public string Strength { get; set; }

        public string History { get; set; }

        public double? TotalUnitNumber { get; set; }

        public string ManagerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }      

        public double? InitialInvestmentEUR { get; set; }

        public DateTime? InitialDate { get; set; }       

        public string LiquidityID { get; set; }

        public string ProductStyleID { get; set; }

        public string FundAUM { get; set; }

        public string ProductStatusID { get; set; }

        public string SectionID { get; set; }

        public bool? Visible { get; set; }

        public bool? Underlying { get; set; }

        public double? Multiplier { get; set; }

        // ProductCapital
        public double InitialCapital { get; set; }

        // ProductFX       
        public string InitialCurrency { get; set; }

        public string EndCurrency { get; set; }

    }
}
