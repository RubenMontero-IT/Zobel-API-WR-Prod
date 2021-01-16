using Newtonsoft.Json;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow
{
    public class Values
    {
        [JsonProperty("PeriodID")]
        public string PeriodId { get; set; }

        [JsonProperty("Value")]
        public double? Value { get; set; }
    }



}
