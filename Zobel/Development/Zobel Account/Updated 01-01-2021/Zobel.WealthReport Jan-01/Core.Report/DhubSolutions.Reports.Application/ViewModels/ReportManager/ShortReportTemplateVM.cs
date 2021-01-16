using DhubSolutions.Reports.Application.ViewModels.ReportManager.Base;
using System;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ShortReportTemplateVM : ShortBaseVM
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
