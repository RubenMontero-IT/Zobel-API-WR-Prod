using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportTemplateElementVM

    {
        public ReportTemplateElementVM()
        {
            Children = new List<ReportTemplateElementVM>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JObject Config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ReportTemplateElementVM> Children { get; set; }
    }
}
