using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportDataUpdatedDto
    {
        public string ReportId { get; set; }

        public IEnumerable<string> DataBloks { get; set; }

    }
}
