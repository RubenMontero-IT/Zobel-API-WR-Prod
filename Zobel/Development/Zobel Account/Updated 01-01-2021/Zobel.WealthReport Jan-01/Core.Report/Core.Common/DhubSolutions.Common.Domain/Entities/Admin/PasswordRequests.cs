using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class PasswordRequests
    {
        public string RequestId { get; set; }
        public string Userid { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Active { get; set; }

        public User User { get; set; }
    }
}
