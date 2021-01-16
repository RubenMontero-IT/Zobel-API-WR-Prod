using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Compare
    {
        [JsonProperty("DataLevelID")]
        public string DataLevelId { get; set; }

        [JsonProperty("DataLevelName")]
        public string DataLevelName { get; set; }

        [JsonProperty("ReportTypes")]
        public ReportType[] ReportTypes { get; set; }
    }



}
