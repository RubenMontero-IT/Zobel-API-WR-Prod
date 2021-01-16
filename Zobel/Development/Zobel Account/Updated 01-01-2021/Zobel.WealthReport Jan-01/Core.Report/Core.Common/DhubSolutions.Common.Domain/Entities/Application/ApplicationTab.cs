using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class ApplicationTab : BaseEntity

    {
        public ApplicationTab() : base()
        {

        }
        public int Name { get; set; }

        public int ApplicationId { get; set; }

        public int ParentTabId { get; set; }

        public bool HasChildren { get; set; }

        public IEnumerable<ApplicationTab> Tabs { get; set; }
    }
}