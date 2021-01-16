using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class SystemGroup : BaseEntity
    {
        public SystemGroup() : base()
        {
            SystemGroupMemberShips = new HashSet<SystemGroupMemberShip>();
        }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<SystemGroupMemberShip> SystemGroupMemberShips { get; set; }
    }


}
