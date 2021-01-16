using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class SignableStatementVM
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool SignedOff { get; set; }

        public ICollection<StatementSignerVM> Signers { get; set; }


    }
}



