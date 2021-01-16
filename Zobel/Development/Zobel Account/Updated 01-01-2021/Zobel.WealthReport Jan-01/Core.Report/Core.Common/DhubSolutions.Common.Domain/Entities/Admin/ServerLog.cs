using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class ServerLog : BaseEntity
    {
        public ServerLog() : base()
        {

        }
        public DateTime CreationDate { get; set; }

        public string Sender { get; set; }

        public string LogBody { get; set; }
    }
}
