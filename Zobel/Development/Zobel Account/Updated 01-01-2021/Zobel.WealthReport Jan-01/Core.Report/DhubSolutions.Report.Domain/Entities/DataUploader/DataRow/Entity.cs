using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Entity
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("AccountID")]
        public long AccountId { get; set; }

        [JsonProperty("Consolidated")]
        public bool Consolidated { get; set; }

        [JsonProperty("Rows")]
        public EntityRow[] Rows { get; set; }
    }



}
