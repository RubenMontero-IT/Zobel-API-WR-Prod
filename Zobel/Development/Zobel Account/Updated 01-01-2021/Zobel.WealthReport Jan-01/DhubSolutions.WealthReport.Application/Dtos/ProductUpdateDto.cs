using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductUpdateDto
    {
        public ProductGeneralInfoDto ProductGeneralInfo { get; set; }

        public IEnumerable<ProductHistoricalInfoCollectionDto> ProductHistoricalInfoCollection { get; set; }

        public IEnumerable<ProductTransactionsCollectionDto> transactions { get; set; }

        public IEnumerable<ProductDto> ParentPortfolio { get; set; }

    }
}
