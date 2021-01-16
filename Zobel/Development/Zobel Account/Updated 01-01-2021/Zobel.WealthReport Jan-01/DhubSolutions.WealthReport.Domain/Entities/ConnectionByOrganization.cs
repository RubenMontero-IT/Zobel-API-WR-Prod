using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class ConnectionByOrganization : BaseEntity
    {
        public ConnectionByOrganization() : base()
        { }

        public string ConnectionID { get; set; }

    }
}
