using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class AppSettingRoleVOrg : BaseEntity
    {
        public AppSettingRoleVOrg() : base()
        {

        }

        public string Rvid { get; set; }
        public string Orgid { get; set; }
        public string SettingId { get; set; }
        public string Value { get; set; }
        public OrganizationRole OrganizationRole { get; set; }
        public AppSetting Setting { get; set; }

    }
}
