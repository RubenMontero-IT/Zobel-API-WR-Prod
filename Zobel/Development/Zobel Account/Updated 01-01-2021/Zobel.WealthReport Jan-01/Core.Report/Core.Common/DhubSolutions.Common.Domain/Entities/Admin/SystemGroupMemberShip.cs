using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class SystemGroupMemberShip : BaseEntity
    {
        public SystemGroupMemberShip() : base()
        {

        }
        public string SystemGroupId { get; set; }
        public SystemGroup SystemGroup { get; set; }
        public string OrganizationRoleId { get; set; }
        public OrganizationRole OrganizationRole { get; set; }
    }
}
