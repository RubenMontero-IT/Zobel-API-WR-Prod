using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class FluctuationFTE_x : IDataUploaderDataRow
    {
        public string Description { get; set; }
        public decimal? Value { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
