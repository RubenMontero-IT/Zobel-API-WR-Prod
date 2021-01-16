using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Account() : base()
        {
        }
        public string AccountName { get; set; }
    }
}
