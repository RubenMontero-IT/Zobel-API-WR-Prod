using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class CashFlowStatement_x : IDataUploaderDataRow
    {
        public string Date { get; set; }
        public double? CashBOP { get; set; }
        public double? CFOperatingActivities { get; set; }
        public double? Earnings { get; set; }
        public double? DepreciationAmortization { get; set; }
        public double? SumNonCashItems { get; set; }
        public double? PlusInterestBankLoan { get; set; }
        public double? PlusInterestShareholderLoan { get; set; }
        public double? CFInvestingActivities { get; set; }
        public double? IntangibleAssets { get; set; }
        public double? TangibleAssets { get; set; }
        public double? FinancialAssets { get; set; }
        public double? CFFinancingActivities { get; set; }
        public double? ShareholdersEquity { get; set; }
        public double? BankLoan { get; set; }
        public double? MinusInterestBankLoan { get; set; }
        public double? ShareholderLoan { get; set; }
        public double? MinusInterestsShareholderLoan { get; set; }
        public double? MinusDividends { get; set; }
        public double? CashOEP { get; set; }
        public double? ChangeCash { get; set; }
        public double? Adjustments { get; set; }
        public double? IncreaseDebt { get; set; }
        public double? DecreaseDebt { get; set; }
        public double? PlusDividends { get; set; }
        public double? FCFE { get; set; }
        public double? CashFXChanges { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
