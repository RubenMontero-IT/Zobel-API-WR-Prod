using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StatementSigner : BaseEntity
    {
        public StatementSigner() : base()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public string SignedById { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User SignedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SignedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SignableStatementId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SignableStatement SignableStatement { get; set; }
    }
}
