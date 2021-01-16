using Newtonsoft.Json.Linq;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager.Base
{
    public class BaseReportElementVM : BaseViewModel
    {
        public string Type { get; set; }

        public JObject Config { get; set; }

    }
}
