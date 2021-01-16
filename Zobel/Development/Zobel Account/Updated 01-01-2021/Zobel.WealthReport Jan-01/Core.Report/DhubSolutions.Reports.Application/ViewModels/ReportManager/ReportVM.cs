using DhubSolutions.Reports.Application.ViewModels.ReportManager.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.ViewModels.ReportManager
{

    public class ReportVM : BaseReportVM
    {
        public DateTime CreationDate { get; set; }

        public JObject Data { get; set; }

        public ICollection<ReportElementPermissionVM> Content { get; set; }

        public ICollection<string> Permissions { get; set; }
    }
}
