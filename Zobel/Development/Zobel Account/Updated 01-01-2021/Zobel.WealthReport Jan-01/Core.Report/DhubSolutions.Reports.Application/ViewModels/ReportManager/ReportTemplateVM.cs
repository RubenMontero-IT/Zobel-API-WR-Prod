using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportTemplateVM
    {
        public ReportTemplateVM()
        {
            Content = new List<ReportTemplateElementVM>();
        }

        /// <summary>
        /// A Name for the Template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A Description of the Template
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The Metadata of this template
        /// </summary>
        public JObject Metadata { get; set; }

        /// <summary>
        /// Gets or sets the data property
        /// </summary>
        public JObject Data { get; set; }

        /// <summary>
        /// Gets or sets the content of this report.
        /// </summary>
        /// <value>The components.</value>
        public ICollection<ReportTemplateElementVM> Content { get; set; }
    }
}
