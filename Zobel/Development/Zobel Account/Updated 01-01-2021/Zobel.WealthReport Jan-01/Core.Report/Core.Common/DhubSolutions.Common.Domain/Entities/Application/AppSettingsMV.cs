using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class AppSettingsMV : BaseEntity
    {
        public AppSettingsMV() : base()
        {

        }
        public string SettingId { get; set; }
        public string Value { get; set; }
        public AppSetting Setting { get; set; }
    }
}
