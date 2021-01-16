using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class LoansSchedule_x : IDataUploaderDataRow
    {
        public string LoanType { get; set; }
        public string LoanDescription { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string LoanStatus { get; set; }
        public string Period { get; set; }
        public double? Amount { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodId { get; set; }
    }
}
