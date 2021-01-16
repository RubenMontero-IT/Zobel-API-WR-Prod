using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Backlog_x : IDataUploaderDataRow
    {
        public string Description { get; set; }
        public double? CurrentMonth { get; set; }
        public double? SalesAmtCurrYear { get; set; }
        public double? SalesAmtFollYear { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
