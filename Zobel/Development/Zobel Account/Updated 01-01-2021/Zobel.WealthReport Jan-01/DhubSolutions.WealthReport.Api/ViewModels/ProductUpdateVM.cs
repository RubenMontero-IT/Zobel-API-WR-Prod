using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class ProductUpdateVM
    {
        public ProductGeneralInfoVM Info { get; set; }

        public IEnumerable<ProductHistoricalInfoCollectionVM> Data { get; set; }

        public IEnumerable<ProductTransactionsCollectionVM> transactions { get; set; }

        public IEnumerable<ProductVM> ParentPortfolio { get; set; }
    }
}







