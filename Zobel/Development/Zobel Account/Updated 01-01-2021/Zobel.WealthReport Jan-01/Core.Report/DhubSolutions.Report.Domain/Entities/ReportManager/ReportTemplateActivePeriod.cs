using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Reports.Domain.Entities.ReportManager
{
    public class ReportTemplateActivePeriod : BaseEntity
    {
        public string Period { get; set; }

        public bool IsActive { get; set; }

        public string ReportTemplateId { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }


        public bool IsTMinusOne()
        {
            DateTime period = DateTime.Parse(Period);
            DateTime dateTMinusOne = DateTime.Today.AddMonths(-1);

            return period.Year == dateTMinusOne.Year && period.Month == dateTMinusOne.Month;
        }
    }




}
