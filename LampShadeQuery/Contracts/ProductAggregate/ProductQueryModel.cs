using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampShadeQuery.Contracts.ProductAggregate
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string DiscountExpireDate { get; set; }
    }
}
