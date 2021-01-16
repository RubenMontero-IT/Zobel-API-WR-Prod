using DhubSolutions.Core.Domain.Entity;
using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    [JsonObject]
    public class PermissionObjectValue : IObjectValue
    {
        public PermissionObjectValue(string organizationRoleId, string typePermission)
        {
            OrgRoleId = organizationRoleId;
            Type = typePermission;
        }

        public string OrgRoleId { get; }

        public string Type { get; }
    }


}
