using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class ProductTransactionsCollectionVM
    {
        public IEnumerable<TransactionVM> Add { get; set; }

        public IEnumerable<TransactionUpdateVM> Update { get; set; }

        public IEnumerable<ProductVM> Remove { get; set; }
    }
}
