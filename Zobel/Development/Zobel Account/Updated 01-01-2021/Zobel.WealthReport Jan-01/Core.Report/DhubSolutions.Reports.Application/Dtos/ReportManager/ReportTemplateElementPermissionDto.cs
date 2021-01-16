using DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateElementPermissionDto
    {
        public string Id { get; set; }

        public IEnumerable<PermissionObjectValue> Granted { get; set; }

        public IEnumerable<PermissionObjectValue> Denied { get; set; }
    }


}
