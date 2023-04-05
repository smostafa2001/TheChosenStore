using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Domain.ProductPictureAggregate
{
    public class CreateProductPicture
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
