using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductHistoricalInfoDto : BaseProductHistoricalInfoDto
    {
        public IEnumerable<ProductDataDto> ProductData { get; set; }
    }
}
