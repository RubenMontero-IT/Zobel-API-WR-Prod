using DhubSolutions.Common.Domain.Entities.Admin;
using Newtonsoft.Json.Linq;
using System;


namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Base
{
    public interface IBaseReportElement
    {
        string Name { get; set; }

        string Description { get; set; }

        string Code { get; set; }

        DateTime CreationDate { get; set; }

        string CreatedById { get; set; }

        User CreatedBy { get; set; }

        string LastModifiedById { get; set; }

        User LastModifiedBy { get; set; }

        DateTime LastModified { get; set; }

        string DataIndex { get; set; }

        string Config { get; set; }

        JObject ConfigJObject { get; }
    }
}