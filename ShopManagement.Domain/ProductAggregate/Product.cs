using Common.Domain;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAggregate;

public class Product(
    string name, string code, string shortDescription,
    string description, string picture, string pictureAlt,
    string pictureTitle, long categoryId, string slug,
    string keywords, string metaDescription
    ) : EntityBase
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string ShortDescription { get; private set; } = shortDescription;
    public string Description { get; private set; } = description;
    public string Picture { get; private set; } = picture;
    public string PictureAlt { get; private set; } = pictureAlt;
    public string PictureTitle { get; private set; } = pictureTitle;
    public string Slug { get; private set; } = slug;
    public string Keywords { get; private set; } = keywords;
    public string MetaDescription { get; private set; } = metaDescription;
    public long CategoryId { get; private set; } = categoryId;
    public ProductCategory Category { get; private set; }
    public List<ProductPicture> ProductPictures { get; private set; }

    public void Edit(
        string name, string code, string shortDescription,
        string description, string picture, string pictureAlt,
        string pictureTitle, long categoryId, string slug,
        string keywords, string metaDescription)
    {
        Name = name;
        Code = code;
        ShortDescription = shortDescription;
        Description = description;
        if (!string.IsNullOrWhiteSpace(picture))
            Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        CategoryId = categoryId;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
    }
}