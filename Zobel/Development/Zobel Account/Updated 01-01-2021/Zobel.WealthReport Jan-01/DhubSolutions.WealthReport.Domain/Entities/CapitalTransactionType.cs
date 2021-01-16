using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class CapitalTransactionType : BaseEntity
    {
        public CapitalTransactionType() : base()
        {
        }
        public string CapitalTransactionTypeName { get; set; }
    }
}
