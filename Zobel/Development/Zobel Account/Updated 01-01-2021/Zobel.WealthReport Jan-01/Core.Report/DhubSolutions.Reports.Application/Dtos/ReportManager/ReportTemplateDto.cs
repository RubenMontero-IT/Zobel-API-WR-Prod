using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateDto
    {
        public ReportTemplateDto()
        {
            Content = new List<ReportTemplateElementDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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
        /// Gets the Metadata of this report as a JsonObject
        /// </summary>
        public JObject Metadata { get; set; }

        /// <summary>
        /// Gets the Data of this report as a JsonObject
        /// </summary>
        public JObject Data { get; set; }

        /// <summary>
        /// Gets or sets the content of this report.
        /// </summary>
        /// <value>The components.</value>
        public IEnumerable<ReportTemplateElementDto> Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportTemplateElementDto> GetAllContent()
        {
            foreach (var element in Content)
            {
                yield return element;
                foreach (var child in element.GetAllContent())
                {
                    yield return child;
                }
            }
        }

    }
}
