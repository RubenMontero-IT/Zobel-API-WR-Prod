using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class AppSetting : BaseEntity
    {
        public AppSetting() : base()
        {
            AppSettingRoleVorg = new HashSet<AppSettingRoleVOrg>();
            AppSettingsByUser = new HashSet<AppSettingsByUser>();
            AppSettingsMV = new HashSet<AppSettingsMV>();
        }
        public string SettingName { get; set; }
        public string Appid { get; set; }
        public string Description { get; set; }
        public string DefaultValue { get; set; }

        public Apps App { get; set; }
        public ICollection<AppSettingRoleVOrg> AppSettingRoleVorg { get; set; }
        public ICollection<AppSettingsByUser> AppSettingsByUser { get; set; }
        public ICollection<AppSettingsMV> AppSettingsMV { get; set; }
    }
}
