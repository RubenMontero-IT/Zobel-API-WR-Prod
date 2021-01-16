using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportElementPermissionVM : ReportElementVM
    {
        public ICollection<string> Permissions { get; set; }
    }


}
