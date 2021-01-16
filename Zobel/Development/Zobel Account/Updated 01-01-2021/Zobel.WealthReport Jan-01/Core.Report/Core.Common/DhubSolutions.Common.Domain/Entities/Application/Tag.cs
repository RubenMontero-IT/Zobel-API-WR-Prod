using DhubSolutions.Core.Domain.Entity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class Tag : BaseEntity
    {
        public Tag() : base()
        {
            TagParents = new HashSet<Tag>();
        }
        public string TagName { get; set; }
        public string Description { get; set; }
#nullable enable
        public string? TagParent { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Author { get; set; }
        public bool IsType { get; set; }

        public Tag TagParentNavigation { get; set; }
        public ICollection<Tag> TagParents { get; set; }
    }
}
