using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class UploadHistoricalPricesVM
    {
        public string ProductId { get; set; }
        public IEnumerable<ProductDataVM> Data { get; set; }
    }
}
