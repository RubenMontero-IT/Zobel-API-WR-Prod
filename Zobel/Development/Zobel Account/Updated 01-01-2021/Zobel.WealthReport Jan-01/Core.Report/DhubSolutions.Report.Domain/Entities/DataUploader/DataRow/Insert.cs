using System.Collections.Generic;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Insert
    {
        public List<FTE_x> FTE { get; set; }
        public List<CashFlowStatement_x> CFS { get; set; }
        public List<ProjectTracking_x> ProjectTracking { get; set; }
        public List<Top10SaleProjects_x> Pipeline { get; set; }
        public List<Top20Debitors_x> Top20Debitor { get; set; }
        public List<Top20Creditors_x> Top20Creditor { get; set; }
        public List<OrderIncome_x> OrderIncome { get; set; }
        public List<Backlog_x> Backlog { get; set; }
        public List<OperatingExpenses_x> OperatingExpenses { get; set; }
        public List<OperatingLease_x> OperatingLease { get; set; }
        public List<CapitalExpenditure_x> CapitalExpenditure { get; set; }
        public List<FluctuationFTE_x> FluctuationFTE { get; set; }
        public List<LongTermIllnessFTE_x> LongTermIllnessFTE { get; set; }
        public List<CashBalance_x> CashBalance { get; set; }
        public List<LoansSchedule_x> LoansSchedule { get; set; }

    }



}
