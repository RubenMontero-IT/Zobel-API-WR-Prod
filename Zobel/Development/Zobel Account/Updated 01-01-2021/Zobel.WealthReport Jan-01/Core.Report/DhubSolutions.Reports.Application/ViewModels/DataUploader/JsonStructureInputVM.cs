using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow;
using Newtonsoft.Json;

namespace DhubSolutions.Reports.Application.ViewModels.DataUploader
{

    public class JsonStructureInputVM
    {
        [JsonProperty("OrganisationName")]
        public string OrganisationName { get; set; }

        [JsonProperty("OrganisationID")]
        public string OrganisationId { get; set; }

        [JsonProperty("PeriodID")]
        public string PeriodId { get; set; }

        [JsonProperty("Insert")]
        public Insert Insert { get; set; }

        //[JsonProperty("Compare")]
        //public List<Compare> Compare { get; set; }
    }
}
