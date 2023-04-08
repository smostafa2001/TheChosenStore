using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryQuery(ShopContext shopContext) => _shopContext = shopContext;

        public List<ProductCategoryQueryModel> GetProductCategories() => _shopContext.ProductCategories.Select(pc => new ProductCategoryQueryModel
        {
            Id = pc.Id,
            Name = pc.Name,
            Picture = pc.Picture,
            PictureAlt = pc.PictureAlt,
            PictureTitle = pc.PictureTitle,
            Slug = pc.Slug
        }).ToList();
    }
}