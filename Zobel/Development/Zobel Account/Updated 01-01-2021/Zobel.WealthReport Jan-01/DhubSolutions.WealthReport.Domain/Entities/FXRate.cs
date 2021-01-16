using DhubSolutions.Core.Domain.Entity;
using System;

namespace DhubSolutions.WealthReport.Domain.Entities
{
    public class FXRate : BaseEntity
    {
        public FXRate() : base()
        {
        }

        public DateTime Date { get; set; }
        public double FXRateValue { get; set; }
        public string InitialCurrency { get; set; }
        public string EndCurrency { get; set; }
    }
}
