using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class LogUserByApp : BaseEntity
    {
        public LogUserByApp() : base()
        { }

        public string Userid { get; set; }
        public string Appid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string ActionId { get; set; }
    }
}
