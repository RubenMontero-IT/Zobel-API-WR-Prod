using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class EntityRow
    {
        [JsonProperty("Head")]
        public string Head { get; set; }

        [JsonProperty("AccountLevelID")]
        public string[] AccountLevelId { get; set; }

        [JsonProperty("Values")]
        public Values[] Values { get; set; }
    }



}
