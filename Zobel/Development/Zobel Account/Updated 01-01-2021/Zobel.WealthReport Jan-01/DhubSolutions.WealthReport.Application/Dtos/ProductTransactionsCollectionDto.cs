using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductTransactionsCollectionDto
    {
        public IEnumerable<TransactionDto> Add { get; set; }

        public IEnumerable<TransactionUpdateDto> Update { get; set; }

        public IEnumerable<ProductDto> Remove { get; set; }

    }
}
