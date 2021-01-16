using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Top20Debitors_x : IDataUploaderDataRow
    {
        public decimal No { get; set; }
        public string Name { get; set; }
        public decimal? TotalYtdamt { get; set; }
        public decimal? OpenAmt { get; set; }
        public decimal? OverdueAmt { get; set; }
        public decimal? OverduePortion { get; set; }
        public string SoldProduct { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
