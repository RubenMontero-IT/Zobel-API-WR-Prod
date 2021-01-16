using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class SharedComment : BaseEntity
    {
        public SharedComment() : base()
        {

        }
        public string CommentId { get; set; }
        public string Rvid { get; set; }
        public bool? ReadOnly { get; set; }
        public DateTime? Date { get; set; }

        public RoleValue Rv { get; set; }
    }
}
