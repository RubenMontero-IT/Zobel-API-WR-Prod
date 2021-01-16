using System;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductDataDto
    {
        public string MainCurrency { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }       
        
    }
}
