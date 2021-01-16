using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DhubSolutions.WealthReport.Api.ViewModels
{
    public class ProductCreationVM
    {
        [Required]
        public ProductGeneralInfoVM Info { get; set; }

        public IEnumerable<ProductHistoricalInfoVM> Data { get; set; }

        public IEnumerable<TransactionVM> Transactions { get; set; }

        public IEnumerable<ProductVM> ParentPortfolio { get; set; }
    }
}
