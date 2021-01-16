using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Base;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplate : BaseReport, IBaseReportTemplate
    {
        public ReportTemplate() : base()
        {
            Content = new List<ReportTemplateElement>();
            ReportTemplatePermissions = new HashSet<ReportTemplatePermission>();
            ActivePeriods = new HashSet<ReportTemplateActivePeriod>();
        }

        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the content of this report.
        /// </summary>
        /// <value>The components.</value>
        public ICollection<ReportTemplateElement> Content { get; set; }

        /// <summary>
        /// Get or set the permissions of this ReportTemplate
        /// </summary>
        public ICollection<ReportTemplatePermission> ReportTemplatePermissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ReportTemplateActivePeriod> ActivePeriods { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ReportTemplateOrganization> TemplateOrganizations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public bool IsAccessible(Organization organization)
        {
            return TemplateOrganizations.Any(org => org.OrganizationId == organization.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportTemplateElement> GetAllContent()
        {
            foreach (var element in Content)
            {
                foreach (var child in element.GetAllContent())
                {
                    yield return child;
                }
            }
        }







    }
}
