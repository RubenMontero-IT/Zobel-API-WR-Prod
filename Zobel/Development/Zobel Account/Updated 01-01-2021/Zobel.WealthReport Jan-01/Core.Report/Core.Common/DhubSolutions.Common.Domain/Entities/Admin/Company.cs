using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class Company : BaseEntity
    {
        public Company() : base()
        {

        }

        public string CompanyName { get; set; }

        public string CompanyContactPage { get; set; }

    }
}
