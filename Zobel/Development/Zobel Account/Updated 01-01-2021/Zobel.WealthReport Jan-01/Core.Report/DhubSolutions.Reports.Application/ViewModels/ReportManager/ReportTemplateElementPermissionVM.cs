using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportTemplateElementPermissionVM
    {
        public string ReportTemplateElementId { get; set; }

        public ICollection<PermissionVM> Permissions { get; set; }

    }


}
