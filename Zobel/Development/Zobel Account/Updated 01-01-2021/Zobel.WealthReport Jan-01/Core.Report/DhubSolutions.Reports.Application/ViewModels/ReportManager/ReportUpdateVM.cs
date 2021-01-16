using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{
    public class ReportUpdateVM
    {
        public string Name { get; set; }

        public ICollection<ReportElementVM> Added { get; set; }

        public ICollection<ReportElementVM> Updated { get; set; }

        public ICollection<string> Removed { get; set; }
    }
}
