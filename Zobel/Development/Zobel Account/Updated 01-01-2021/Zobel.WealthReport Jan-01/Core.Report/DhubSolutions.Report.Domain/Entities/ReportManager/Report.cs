using DhubSolutions.Reports.Domain.Entities.ReportManager.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class Report : BaseReport
    {
        public Report() : base()
        {
            Content = new List<ReportElement>();
            ReportPermissions = new HashSet<ReportPermission>();
        }

        public string CreationOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PeriodId { get; set; }

        /// <summary>
        /// The id of Template from wich this report was created
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// The template from wich this report was created
        /// </summary>
        public ReportTemplate Template { get; set; }

        /// <summary>
        /// Get or Set the permissions of this report
        /// </summary>
        public ICollection<ReportPermission> ReportPermissions { get; set; }

        /// <summary>
        /// Gets or sets the content of this report.
        /// </summary>
        /// <value>The components.</value>
        public ICollection<ReportElement> Content { get; set; }

        /// <summary>
        /// Indexes the report and returns the element with the provided Id or null otherwise
        /// </summary>
        /// <param name="id">the id of the element to be found</param>
        /// <returns>The element corresponding the provided id or null otherwise</returns>
        public ReportElement GetContentElement(string id)
        {
            IndexReport();
            //return this[id];
            return null;
        }

        /// <summary>
        /// Creates an index with al the childrens of this report
        /// </summary>
        /// <returns>A copy of the internal dictionary used for indexing the elements</returns>
        public void IndexReport()
        {
            Dictionary<string, ReportElement> resultantIndex = new Dictionary<string, ReportElement>();
            List<ReportElement> resultantNewElements = new List<ReportElement>();
            //Pass the dict for indexing 
            foreach (var item in Content)
            {
                item.IndexReportElements(resultantIndex, resultantNewElements);
            }
            //this.TreeElements = resultantIndex;
            //this.NewElements = resultantNewElements;


        }

        /// <summary>
        /// Updates the references of each element to its parent
        /// </summary>
        /// <returns>True if all elements where updated ok, False if something whent wrong</returns>
        public bool UpdateUpReferences()
        {
            bool result = true;
            //The id of this report for some reason its not been set
            if (string.IsNullOrEmpty(Id) || string.IsNullOrWhiteSpace(Id))
                return false;

            foreach (var child in Content)
            {
                child.ReportId = Id;
                result &= child.UpdateUpReferences(Id, null);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creationOptions"></param>
        public void SetCreationOptions(Dictionary<string, dynamic> creationOptions)
        {
            CreationOptions = JsonConvert.SerializeObject(creationOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetCreationOptions()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(CreationOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportElement> GetAllContent()
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
