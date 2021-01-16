using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Apps : BaseEntity
    {
        public Apps() : base()
        {
            AppMaxPermission = new HashSet<AppMaxPermission>();
            AppSetting = new HashSet<AppSetting>();
            RoleAppPermission = new HashSet<RoleAppPermission>();
        }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string Description { get; set; }
        public bool AppIsWeb { get; set; }
        public string AppUrl { get; set; }
        public bool IsEnable { get; set; }
        public string AppLogo { get; set; }
        public string AlternativeName { get; set; }
        public bool InternalUse { get; set; }

        public ICollection<AppMaxPermission> AppMaxPermission { get; set; }
        public ICollection<AppSetting> AppSetting { get; set; }
        public ICollection<RoleAppPermission> RoleAppPermission { get; set; }
    }
}
