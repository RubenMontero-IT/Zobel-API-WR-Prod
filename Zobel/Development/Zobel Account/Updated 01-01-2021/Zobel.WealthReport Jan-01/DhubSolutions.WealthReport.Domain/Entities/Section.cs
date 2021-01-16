using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Section : BaseEntity
    {
        public Section() : base()
        {
        }
        public string SectionName { get; set; }
    }
}
