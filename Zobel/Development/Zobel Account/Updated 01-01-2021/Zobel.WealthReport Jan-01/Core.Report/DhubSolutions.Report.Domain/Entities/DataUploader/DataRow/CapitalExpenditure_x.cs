using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class CapitalExpenditure_x : IDataUploaderDataRow
    {
        public int? No { get; set; }
        public string InvestmentType { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public double? CurrentMonth { get; set; }
        public double? YTD { get; set; }
        public double? BudgetTY { get; set; }
        public double? ForecastTY { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
