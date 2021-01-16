using System;
using System.Collections.Generic;
using System.Text;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class UploadHistoricalPricesDto
    {
        public string ProductId { get; set; }
        public IEnumerable<ProductDataDto> Data { get; set; }
    }
}
