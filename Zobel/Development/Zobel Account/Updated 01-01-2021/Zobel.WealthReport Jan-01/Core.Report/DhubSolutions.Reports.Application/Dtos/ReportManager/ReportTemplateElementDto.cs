using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateElementDto
    {
        public string Id { get; set; }

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
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JObject Config { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ReportTemplateElementDto> Children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportTemplateElementDto> GetAllContent()
        {
            foreach (var item in Children)
            {
                yield return item;
                foreach (var item1 in item.GetAllContent())
                {
                    yield return item1;
                }
            }
        }

    }
}
