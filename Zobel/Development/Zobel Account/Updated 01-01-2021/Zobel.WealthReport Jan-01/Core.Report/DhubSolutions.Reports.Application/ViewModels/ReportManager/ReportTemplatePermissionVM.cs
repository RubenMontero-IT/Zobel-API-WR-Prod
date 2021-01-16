using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportTemplatePermissionVM
    {
        public ICollection<PermissionVM> Permissions { get; set; }

        public ICollection<ReportTemplateElementPermissionVM> ReportTemplateElementPermissions { get; set; }
    }


}
