using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class ProjectTracking_x : IDataUploaderDataRow
    {
        public int ProjectNo { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Actions { get; set; }
        public string Responsibility { get; set; }
        public decimal? Impact { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
