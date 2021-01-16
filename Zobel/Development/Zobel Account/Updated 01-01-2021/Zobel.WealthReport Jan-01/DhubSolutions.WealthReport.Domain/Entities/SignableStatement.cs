using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SignableStatement : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public SignableStatement() : base()
        {
            StatementSigners = new List<StatementSigner>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string StatementCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StatementCategory StatementCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReportTemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ReportTemplate ReportTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<StatementSigner> StatementSigners { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsSignedOff()
        {
            return StatementSigners.Count(stamentSigner => stamentSigner.SignedById != null) == 2;
            //return SignedById != null;
        }
    }
}
