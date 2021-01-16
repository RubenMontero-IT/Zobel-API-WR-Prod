using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Country : BaseEntity
    {
        public Country() : base()
        {
        }

        public string CountryAcronym { get; set; }

    }
}
