using System;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class Session
    {
        public string Id { get; set; }
        public string Userid { get; set; }
        public DateTime Access { get; set; }
        public string Data { get; set; }
        public DateTime? Expires { get; set; }
    }
}
