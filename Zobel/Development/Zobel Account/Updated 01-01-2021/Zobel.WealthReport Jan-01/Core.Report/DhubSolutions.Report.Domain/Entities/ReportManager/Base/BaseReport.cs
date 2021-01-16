using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using Newtonsoft.Json.Linq;
using System;


namespace DhubSolutions.Reports.Domain.Entities.ReportManager.Base
{
    public abstract class BaseReport : BaseEntity, IBaseReport
    {

        protected BaseReport() : base()
        {

        }

        /// <summary>
        /// A Description of the Template
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Metadata of this template
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// Gets the Metadata of this report as a JsonObject
        /// </summary>
        public JObject MetadataJObject
        {
            get
            {
                if (string.IsNullOrEmpty(Metadata) || string.IsNullOrWhiteSpace(Metadata))
                {
                    return null;
                }

                try
                {
                    return JObject.Parse(Metadata);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Json Parse Fail on Prop Config, From component with Id =" + Id);
                    return null;
                }
            }
        }

        /// <summary>
        /// A Name for the Template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        /// <value>The visibility.</value>
        public string Visibility { get; set; }

        /// <summary>
        /// Gets or sets the data property
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets the Data of this report as a JsonObject
        /// </summary>
        public JObject DataJObject
        {
            get
            {
                if (string.IsNullOrEmpty(Data) || string.IsNullOrWhiteSpace(Data))
                {
                    return null;
                }

                try
                {
                    return JObject.Parse(Data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Json Parse Fail on Prop Data, From component with Id =" + Id);
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Id of the creator
        /// </summary>
        public string CreatedById { get; set; }

        /// <summary>
        /// The navigation property for the creator
        /// </summary>
        public User CreatedBy { get; set; }

        /// <summary>
        /// The date in witch was created
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The date in witch was last modified
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
