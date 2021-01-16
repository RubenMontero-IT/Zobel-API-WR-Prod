using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class AssetClass : BaseEntity
    {
        public AssetClass() : base()
        {
        }
        public string AssetClassName { get; set; }
    }
}
