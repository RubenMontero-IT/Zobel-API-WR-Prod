using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class UserRoleOrg : BaseEntity
    {
        public string Userid { get; set; }
        public string Rvid { get; set; }
        public string Orgid { get; set; }

        public OrganizationRole OrganizationRole { get; set; }
        public User User { get; set; }
    }
}