using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Organization_WR : BaseEntity
    {
        public Organization_WR() : base()
        { }

        public int? LucanetId { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationDescription { get; set; }

        public string ResourceId { get; set; }

        public string CurrencyId { get; set; }


    }
}
