using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Top10SaleProjects_x : IDataUploaderDataRow
    {
        public int ProjectNo { get; set; }
        public string Customer { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double? Volume { get; set; }
        public decimal? Probability { get; set; }
        public decimal? CurrentYearSales { get; set; }
        public decimal? NextYearSales { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
