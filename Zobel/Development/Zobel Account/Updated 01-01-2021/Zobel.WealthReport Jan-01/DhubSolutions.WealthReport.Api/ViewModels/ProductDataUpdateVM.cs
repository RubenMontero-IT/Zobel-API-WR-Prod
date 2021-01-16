using System;

namespace DhubSolutions.WealthReport.Api.ViewModels
{

    public class ProductDataUpdateVM
    {
        public string MainCurrency { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }        

        public string id { get; set; }
    }
}
