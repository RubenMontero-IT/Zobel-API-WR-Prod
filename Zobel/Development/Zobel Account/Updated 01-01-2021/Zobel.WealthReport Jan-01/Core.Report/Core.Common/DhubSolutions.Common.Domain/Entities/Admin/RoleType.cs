using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class RoleType : BaseEntity
    {
        public RoleType() : base()
        {
            RoleValues = new HashSet<RoleValue>();
        }
        public string RoleTypeName { get; set; }
        public string Description { get; set; }
        public ICollection<RoleValue> RoleValues { get; set; }
    }
}
