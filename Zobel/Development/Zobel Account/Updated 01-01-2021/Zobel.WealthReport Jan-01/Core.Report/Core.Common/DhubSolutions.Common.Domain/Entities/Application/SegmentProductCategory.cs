using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class SegmentProductCategory : BaseEntity
    {
        public SegmentProductCategory() : base()
        {
        }
        public string SegProdCatValue { get; set; }
        public string Description { get; set; }
    }
}
