using Newtonsoft.Json.Linq;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager.Base
{
    public class BaseReportVM : BaseViewModel
    {
        /// <summary>
        /// A Description of the Template
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Metadata of this template
        /// </summary>
        public JObject Metadata { get; set; }

        /// <summary>
        /// A Name for the Template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Id of the creator
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
