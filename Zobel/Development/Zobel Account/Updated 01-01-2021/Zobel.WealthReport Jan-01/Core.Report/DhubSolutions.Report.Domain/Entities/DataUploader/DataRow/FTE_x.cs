using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class FTE_x : IDataUploaderDataRow
    {
        public string Description { get; set; }
        public double? Value { get; set; }
        public string PeriodId { get; set; }
        public string OrganisationId { get; set; }
    }
}
