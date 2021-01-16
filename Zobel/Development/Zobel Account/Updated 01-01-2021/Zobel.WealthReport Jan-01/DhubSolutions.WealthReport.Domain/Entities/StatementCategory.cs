using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class StatementCategory : BaseEntity
    {
        public StatementCategory() : base()
        { }

        public string StatementCategoryName { get; set; }

        public string Description { get; set; }
    }
}
