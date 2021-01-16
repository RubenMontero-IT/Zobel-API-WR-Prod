using System;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class StatementSignerVM
    {
        public string SignedBy { get; set; }
        public DateTime? SignedDate { get; set; }
    }
}
