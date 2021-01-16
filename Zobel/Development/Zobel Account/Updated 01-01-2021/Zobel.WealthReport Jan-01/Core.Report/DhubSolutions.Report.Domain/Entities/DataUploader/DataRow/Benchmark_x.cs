using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Benchmark_x : IDataUploaderDataRow
    {
        public string Benchmark { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public decimal? Value { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
