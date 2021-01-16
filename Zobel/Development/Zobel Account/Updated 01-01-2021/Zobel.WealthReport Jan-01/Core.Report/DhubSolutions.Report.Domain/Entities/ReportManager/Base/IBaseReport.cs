using DhubSolutions.Common.Domain.Entities.Admin;
using Newtonsoft.Json.Linq;
using System;


namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Base
{
    public interface IBaseReport
    {
        string Description { get; set; }
        string Metadata { get; set; }
        JObject MetadataJObject { get; }
        string Name { get; set; }
        string Visibility { get; set; }
        string Data { get; set; }
        JObject DataJObject { get; }
        string CreatedById { get; set; }
        User CreatedBy { get; set; }
        DateTime CreationDate { get; set; }
        DateTime LastModified { get; set; }
    }
}