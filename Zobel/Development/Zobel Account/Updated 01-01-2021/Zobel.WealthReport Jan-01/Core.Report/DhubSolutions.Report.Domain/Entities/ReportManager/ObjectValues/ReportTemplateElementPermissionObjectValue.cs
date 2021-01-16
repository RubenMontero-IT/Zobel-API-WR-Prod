using DhubSolutions.Core.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues
{
    public class ReportTemplateElementPermissionObjectValue : IObjectValue
    {
        public ReportTemplateElementPermissionObjectValue(
            string elementId,
            IEnumerable<PermissionObjectValue> permissions)
        {
            ElementId = elementId;
            Permissions = permissions.ToList();
        }
        public string ElementId { get; }

        public IEnumerable<PermissionObjectValue> Permissions { get; }
    }
}
