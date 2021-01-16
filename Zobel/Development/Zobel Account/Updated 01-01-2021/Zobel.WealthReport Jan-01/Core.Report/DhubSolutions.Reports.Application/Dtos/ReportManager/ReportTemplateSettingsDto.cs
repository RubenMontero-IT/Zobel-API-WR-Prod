using Newtonsoft.Json.Linq;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateSettingsDto
    {
        public string UserId { get; set; }

        public string OrgRoleId { get; set; }

        public string ReportId { get; set; }

        public JObject Settings { get; set; }
    }
}
