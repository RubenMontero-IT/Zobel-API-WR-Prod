using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class OrderIncome_x : IDataUploaderDataRow
    {
        public string Description { get; set; }
        public decimal? CurrentMonth { get; set; }
        public decimal? Ytd { get; set; }
        public decimal? SalesAmtCurrYear { get; set; }
        public decimal? SalesAmtFollYear { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
