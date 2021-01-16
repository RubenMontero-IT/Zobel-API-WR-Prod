using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class ReportType
    {
        [JsonProperty("WorkspaceID")]
        public string WorkspaceId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Entities")]
        public Entity[] Entities { get; set; }
    }



}
