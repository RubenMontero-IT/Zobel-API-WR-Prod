using DhubSolutions.Reports.Domain.Entities.ReportManager.Base;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplateElement : BaseReportElement
    {
        public ReportTemplateElement() : base()
        {
            Children = new List<ReportTemplateElement>();
            ReportTemplateElementPermissions = new HashSet<ReportTemplateElementPermission>();
        }

        //public int ElementIndex { get; set; }

        public string ReportTemplateId { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public string ContainerId { get; set; }

        public ReportTemplateElement Container { get; set; }

        /// <summary>
        /// The children of this element if empty the is a leaf element (pure template element)
        /// </summary>
        public ICollection<ReportTemplateElement> Children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<ReportTemplateElement> GetAllContent()
        {
            yield return this;

            foreach (var element in Children)
            {
                foreach (var child in element.GetAllContent())
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// Get or Set the permissions of this ReportTemplateElement
        /// </summary>
        public ICollection<ReportTemplateElementPermission> ReportTemplateElementPermissions { get; set; }


    }
}
