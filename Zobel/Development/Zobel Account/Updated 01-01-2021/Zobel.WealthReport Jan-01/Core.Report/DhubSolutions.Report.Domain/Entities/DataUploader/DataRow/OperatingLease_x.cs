using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class OperatingLease_x : IDataUploaderDataRow
    {
        public int? No { get; set; }
        public string InvestmentType { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public decimal? CurrentMonth { get; set; }
        public decimal? Ytd { get; set; }
        public decimal? BudgetTy { get; set; }
        public decimal? ForecastTy { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
