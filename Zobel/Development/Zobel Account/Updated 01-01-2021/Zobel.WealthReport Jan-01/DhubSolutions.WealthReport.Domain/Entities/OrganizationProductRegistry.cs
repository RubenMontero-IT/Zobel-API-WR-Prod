using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class OrganizationProductRegistry : BaseEntity
    {

        public OrganizationProductRegistry() : base()
        {
            OrganizationProductExtendedRegistries = new HashSet<OrganizationProductExtendedRegistry>();
        }
        public string ProductID { get; set; }
        public string OrganizationID { get; set; }
        public double NumberOfUnits { get; set; }
        public DateTime Date { get; set; }

        public OrganizationProduct OrganizationProduct { get; set; }
        public ICollection<OrganizationProductExtendedRegistry> OrganizationProductExtendedRegistries { get; set; }
    }
}
