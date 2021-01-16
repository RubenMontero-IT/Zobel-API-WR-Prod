using System.Collections.Generic;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    [JsonObject]
    public class ReportDataSegment
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InstructionProcessorType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("params")]
        public List<ReportDataParam> Params { get; set; }

        [JsonProperty("sql")]
        public string Sql { get; set; }

        [JsonProperty("value")]
        public dynamic Value { get; set; }
    }
}
