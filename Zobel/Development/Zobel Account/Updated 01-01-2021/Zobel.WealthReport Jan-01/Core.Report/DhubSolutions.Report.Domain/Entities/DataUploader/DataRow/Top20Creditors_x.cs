using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Top20Creditors_x : IDataUploaderDataRow
    {
        public int No { get; set; }
        public string Name { get; set; }
        public decimal? TotalYtdamt { get; set; }
        public decimal? OpenAmt { get; set; }
        public decimal? OverdueAmt { get; set; }
        public decimal? OverduePortion { get; set; }
        public string OrderedProduct { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
