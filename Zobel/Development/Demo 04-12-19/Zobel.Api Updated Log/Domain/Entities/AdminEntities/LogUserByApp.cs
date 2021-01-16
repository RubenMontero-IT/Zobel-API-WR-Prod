using Domain.Entities.Abstractions;
using System;

namespace Domain.Entities.DbInvestmentEntities
{
    public class LogUserByApp : BaseEntity
    {
        public string Userid { get; set; }
        public string Appid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string? ActionId { get; set; }
    }
}
