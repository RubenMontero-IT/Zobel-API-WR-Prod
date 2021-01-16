using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class ProductDataCollectionVM
    {
        public IEnumerable<ProductDataVM> Add { get; set; }

        public IEnumerable<ProductDataUpdateVM> Update { get; set; }

        public IEnumerable<ProductVM> Remove { get; set; }
    }
}
