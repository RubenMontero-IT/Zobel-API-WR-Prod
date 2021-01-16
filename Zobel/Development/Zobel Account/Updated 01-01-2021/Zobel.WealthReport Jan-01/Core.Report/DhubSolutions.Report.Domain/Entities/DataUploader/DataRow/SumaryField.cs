namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class SumaryField
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public string OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string WorkspaceId { get; set; }
        public double? Value { get; set; }
        public string PeriodId { get; set; }
        public string PeriodType { get; set; }
        public string AccountLevel { get; set; }
        public string AccountLevelId { get; set; }
        public int Alevel { get; set; }
        public string DataLevelName { get; set; }
        public long DataLevelId { get; set; }
    }
}
