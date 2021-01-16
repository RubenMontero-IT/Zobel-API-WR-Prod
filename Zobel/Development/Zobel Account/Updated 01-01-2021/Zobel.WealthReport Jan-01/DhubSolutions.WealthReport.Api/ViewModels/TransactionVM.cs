using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class TransactionVM
    {
        public string TransactionTypeID { get; set; }
        public string CapitalTransactionTypeID { get; set; }
        public double Amount { get; set; }
        public double NumberOfUnits { get; set; }
        public string Currency { get; set; }
        public double? Price { get; set; }
        public double? FXRate { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? TDatetime { get; set; }        
    }
}
