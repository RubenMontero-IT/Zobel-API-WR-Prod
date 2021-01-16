using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplatePermissionDto
    {
        public IEnumerable<JObject> Granted { get; set; }

        public IEnumerable<JObject> Denied { get; set; }
    }
}