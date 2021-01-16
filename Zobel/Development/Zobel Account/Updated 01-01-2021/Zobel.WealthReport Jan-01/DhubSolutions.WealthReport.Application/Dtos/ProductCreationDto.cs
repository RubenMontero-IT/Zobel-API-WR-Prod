using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductCreationDto
    {
        public ProductGeneralInfoDto ProductGeneralInfo { get; set; }

        public IEnumerable<ProductHistoricalInfoDto> ProductHistoricalInfo { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }

        public IEnumerable<ProductDto> ParentPortfolio { get; set; }
    }
}
