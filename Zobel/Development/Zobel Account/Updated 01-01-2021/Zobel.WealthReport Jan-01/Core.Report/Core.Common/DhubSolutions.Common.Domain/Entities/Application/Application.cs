using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Application : BaseEntity
    {
        public Application() : base()
        {

        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool Avaliable { get; set; }

        public bool IsMultiTab { get; set; }

        public IEnumerable<ApplicationTab> Tabs { get; set; }
    }
}
