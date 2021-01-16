using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class TransactionType : BaseEntity
    {
        public TransactionType() : base()
        {
        }
        public string TransactionTypeName { get; set; }
    }
}
