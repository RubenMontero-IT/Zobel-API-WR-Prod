using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public Currency() : base()
        {
            OrganizationProductExtendedRegistryBaseCurrencies = new HashSet<OrganizationProductExtendedRegistry>();
            OrganizationProductExtendedRegistryMainCurrencies = new HashSet<OrganizationProductExtendedRegistry>();
            Products = new HashSet<Product>();
            ProductExtendedRegistryBaseCurrencies = new HashSet<ProductExtendedRegistry>();
            ProductExtendedRegistryMainCurrencies = new HashSet<ProductExtendedRegistry>();
        }

        public string CurrencyName { get; set; }
        public ICollection<OrganizationProductExtendedRegistry> OrganizationProductExtendedRegistryBaseCurrencies { get; set; }
        public ICollection<OrganizationProductExtendedRegistry> OrganizationProductExtendedRegistryMainCurrencies { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductExtendedRegistry> ProductExtendedRegistryBaseCurrencies { get; set; }
        public ICollection<ProductExtendedRegistry> ProductExtendedRegistryMainCurrencies { get; set; }
    }
}
