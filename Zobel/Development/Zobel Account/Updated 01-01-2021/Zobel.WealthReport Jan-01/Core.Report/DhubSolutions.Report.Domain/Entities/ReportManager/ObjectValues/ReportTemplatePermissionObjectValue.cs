using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    public class ReportTemplatePermissionObjectValue : IObjectValue
    {
        public ReportTemplatePermissionObjectValue(
            IEnumerable<PermissionObjectValue> permissions,
            IEnumerable<ReportTemplateElementPermissionObjectValue> reportTemplateElementPermissions)
        {
            TemplatePermissions = permissions.ToList();
            ElementsPermissions = reportTemplateElementPermissions.ToList();
        }
        public IEnumerable<PermissionObjectValue> TemplatePermissions { get; }

        public IEnumerable<ReportTemplateElementPermissionObjectValue> ElementsPermissions { get; }


    }
}
