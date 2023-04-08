using DiscountManagement.Domain.Shared;
using ShopManagement.Domain.ProductAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Domain.ColleagueDiscountAggregate
{
    public class DefineColleagueDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}