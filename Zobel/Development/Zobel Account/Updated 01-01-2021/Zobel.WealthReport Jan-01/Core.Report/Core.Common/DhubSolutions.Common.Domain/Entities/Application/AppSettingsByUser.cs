using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class AppSettingsByUser : BaseEntity
    {
        public AppSettingsByUser() : base()
        {

        }
        public string SettingId { get; set; }
        public string Userid { get; set; }
        public string Value { get; set; }
        public AppSetting Setting { get; set; }
        public User User { get; set; }

    }
}
