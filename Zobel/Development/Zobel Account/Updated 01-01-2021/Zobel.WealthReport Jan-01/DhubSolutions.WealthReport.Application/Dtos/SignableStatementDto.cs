using System;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class SignableStatementDto
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool SignedOff { get; set; }

        public string SignedBy { get; set; }       

        public DateTime? SignedDate { get; set; }


    }
}
