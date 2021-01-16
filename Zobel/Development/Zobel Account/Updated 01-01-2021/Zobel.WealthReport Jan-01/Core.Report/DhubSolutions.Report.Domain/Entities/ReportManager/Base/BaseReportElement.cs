using System;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using Newtonsoft.Json.Linq;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Base
{
    public abstract class BaseReportElement : BaseEntity, IBaseReportElement
    {
        protected BaseReportElement() : base()
        {

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public string LastModifiedById { get; set; }

        public User LastModifiedBy { get; set; }

        public DateTime LastModified { get; set; }

        public string DataIndex { get; set; }

        public string Config { get; set; }

        public JObject ConfigJObject
        {
            get
            {
                if (string.IsNullOrEmpty(Config) || string.IsNullOrWhiteSpace(Config))
                {
                    return null;
                }

                try
                {
                    return JObject.Parse(Config);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Json Parse Fail on Prop Config, From component with Id =" + Id);
                    return null;
                }
            }
        }

    }
}
