using Newtonsoft.Json;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    [JsonObject]
    public class PermissionDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("orgRoleId")]
        public string OrgRoleId { get; set; }

        [JsonProperty("orgId")]
        public string OrgId { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }
    }
}
