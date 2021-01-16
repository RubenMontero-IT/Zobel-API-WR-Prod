using DhubSolutions.Reports.Application.ViewModels.ReportManager.Base;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportElementVM : BaseReportElementVM
    {
        /// <summary>
        /// 
        /// </summary>
        public string ContainerId { get; set; }

        /// <summary>
        /// The children of this element if empty the is a leaf element (pure element)
        /// </summary>
        public ICollection<ReportElementVM> Children { get; set; }
    }


}
