using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class ProductHistoricalInfoVM : BaseProductHistoricalInfoVM
    {
        public IEnumerable<ProductDataVM> Data { get; set; }
    }
}
