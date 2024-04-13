using DecorativeStoreQuery.Contracts.ProductAggregate;
using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.ProductCategoryAggregate;

public class ProductCategoryQueryModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public string Keywords { get; set; }
    public string MetaDescription { get; set; }
    public List<ProductQueryModel> Products { get; set; }
}