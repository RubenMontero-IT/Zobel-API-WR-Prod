using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Dtos
{
    public class ProductDataCollectionDto
    {
        public IEnumerable<ProductDataDto> Add { get; set; }

        public IEnumerable<ProductDataUpdateDto> Update { get; set; }

        public IEnumerable<ProductDto> Remove { get; set; }


    }
}
